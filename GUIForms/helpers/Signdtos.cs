using Domain.Models;
using GUIForms.helpers;
using java.security.cert;
using javax.xml.validation;
using Newtonsoft.Json;
using org.apache.xerces.xni;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using UOW;
using Zatca.EInvoice.SDK;
using Zatca.EInvoice.SDK.Contracts;

namespace GUI.Helpers
{
    public class Signdtos
    {
        private readonly EInvoiceValidator _validator;
        private readonly EInvoiceSigner _signer;
        private readonly RequestGenerator _requestGenerator;
        UBLClass UC;
        IUnitofwork _IUW;
        public string QR { get; set; }
        public int Saleid { get; set; }
        public int Ublid { get; set; }
        public Signdtos()
        {
            _validator = new EInvoiceValidator();
            _signer = new EInvoiceSigner();
            _requestGenerator = new RequestGenerator();
            UC = new UBLClass();
            _IUW = new Unitofwork(new EasyposEntities());
        }
        public async Task Sign(XmlDocument doc,string invtitle)
        {
            // Json File Read
            string Certjson = File.ReadAllText("Data/Certificates/CSR.Json");
            var certData = JsonConvert.DeserializeObject<CSR>(Certjson);
            // شهادة
            string base64Cert = certData.binarySecurityToken;
            byte[] certBytes = Convert.FromBase64String(base64Cert);
            string certPem = Encoding.UTF8.GetString(certBytes);

            // مفتاح خاص
            string base64Key = File.ReadAllText("Data/Certificates/private.key");
            byte[] keyBytes = Convert.FromBase64String(base64Key);
            string keyPem = Encoding.UTF8.GetString(keyBytes);

            // تحميل الشهادة بالطريقة الصحيحة
            var certdata = new X509Certificate2(Encoding.UTF8.GetBytes(certPem));
            var issuer = certdata.Issuer;
            var serial = certdata.SerialNumber;
            Console.WriteLine(certdata + "\n" + issuer + "\n" + serial);
            var result = _signer.SignDocument(doc, certPem, keyPem);
            if (!result.IsValid)
            {
                Console.WriteLine("❌ Signing Failed");
                foreach (var error in result.Steps)
                    Console.WriteLine("- " + error.IsValid.ToString() + ":" + error.StepName + " : " + error.ErrorMessages);
                Console.ReadKey();
            }
            else
            {
                //Console.WriteLine("✅ Signed invoice is done scuccess ... ");
                foreach (var error in result.Steps)
                {
                    Console.WriteLine($"- {error.IsValid} : {error.StepName}");
                    if (error.ErrorMessages != null && error.ErrorMessages.Any())
                    {
                        foreach (var msg in error.ErrorMessages)
                        {
                            Console.WriteLine($"    🔴 {msg}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("    ✅ No error messages.");
                    }
                }
                File.WriteAllText("Data/Signedinvoice.xml", result.SignedEInvoice.OuterXml);
                //MessageBox.Show("✅ Signed invoice saved to: " + "Data/Signedinvoice.xml");
                var xmlContent = File.ReadAllText("Data/Signedinvoice.xml");
                var invoiceDoc = LoadInvoiceFromString(xmlContent);
                await GenerateRequest(invoiceDoc, invtitle);
            }

        }
        public async Task GenerateRequest(XmlDocument signedDoc, string invtitle)
        {
            // 1. توليد نتيجة الطلب من SDK
            var result = _requestGenerator.GenerateRequest(signedDoc);
            var xml = XDocument.Load("Data/Signedinvoice.xml"); // أو Load من string باستخدام XDocument.Parse

            // تعريف الـ namespaces
            XNamespace cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            XNamespace cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";

            // البحث عن عنصر QR
            QR = xml
                .Descendants(cac + "AdditionalDocumentReference")
                .FirstOrDefault(x =>
                    x.Element(cbc + "ID")?.Value == "QR")
                ?.Element(cac + "Attachment")
                ?.Element(cbc + "EmbeddedDocumentBinaryObject")
                ?.Value;
            var IH = result.InvoiceRequest.InvoiceHash;
            var Inv = result.InvoiceRequest.Invoice;
            var Uuid = result.InvoiceRequest.Uuid;

            // 2. إعداد الـ NamespaceManager
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(signedDoc.NameTable);
            nsmgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            // 3. تعديل أو إدراج عنصر PIH بين ICV و QR
            var pihNode = signedDoc.SelectSingleNode("//cbc:EmbeddedDocumentBinaryObject[../cbc:ID='PIH']", nsmgr);

            if (pihNode != null)
            {
                pihNode.InnerText = result.InvoiceRequest.InvoiceHash;
                Console.WriteLine("🔁 تم تعديل قيمة PIH داخل العنصر الموجود");
            }
            else
            {
                XmlElement pihRef = signedDoc.CreateElement("cac", "AdditionalDocumentReference", nsmgr.LookupNamespace("cac"));
                XmlElement pihId = signedDoc.CreateElement("cbc", "ID", nsmgr.LookupNamespace("cbc"));
                pihId.InnerText = "PIH";

                XmlElement attachment = signedDoc.CreateElement("cac", "Attachment", nsmgr.LookupNamespace("cac"));
                XmlElement embeddedObj = signedDoc.CreateElement("cbc", "EmbeddedDocumentBinaryObject", nsmgr.LookupNamespace("cbc"));
                embeddedObj.SetAttribute("mimeCode", "text/plain");
                embeddedObj.InnerText = result.InvoiceRequest.InvoiceHash;

                attachment.AppendChild(embeddedObj);
                pihRef.AppendChild(pihId);
                pihRef.AppendChild(attachment);

                // إدراج بعد عنصر ICV
                var icvNode = signedDoc.SelectSingleNode("//cac:AdditionalDocumentReference[cbc:ID='ICV']", nsmgr);
                signedDoc.DocumentElement.InsertAfter(pihRef, icvNode);
            }

            // 4. حفظ النسخة المعدلة
            string path = $"Data/Signedinvoice-{invtitle}-{DateTime.Now.ToString("dd-MM-yyyy")}.xml";
            signedDoc.Save(path);
            //Logger.LogInfo("✅ The amended invoice is saved in : " + path);
            string Oldpathi = @"Data/Invoice.xml";
            string Oldpaths = @"Data/Signedinvoice.xml";
            File.Delete(Oldpathi);
            File.Delete(Oldpaths);
            UC.SaveUBL(QR, IH, Uuid, Inv, Saleid, "لم تسجل", path);


            //var xmlContent = File.ReadAllText("Data/Signedinvoice.xml");
            //var invoiceDoc = LoadInvoiceFromString(xmlContent);

            //// Json File Read
            //string Certjson = File.ReadAllText("Data/Certificates/CSR.Json");
            //var certData = JsonConvert.DeserializeObject<CSR>(Certjson);

            // خطوة 1: تحقق داخلي
            //var validation = _validator.ValidateEInvoice(invoiceDoc, certData.binarySecurityToken, result.InvoiceRequest.InvoiceHash);
            //await SendInvoiceAsync(result.InvoiceRequest.InvoiceHash,
            //     result.InvoiceRequest.Uuid,
            //     result.InvoiceRequest.Invoice);
        }
        public async Task SendInvoiceAsync(string invoiceHash, string uuid, string base64Invoice, string path,string qr)
        {
            var client = new HttpClient();
            var payload = new
            {
                invoiceHash = invoiceHash,
                uuid = uuid,
                invoice = base64Invoice
            };
            // Json File Read
            string Certjson = File.ReadAllText("Data/Certificates/CSR.Json");
            var certData = JsonConvert.DeserializeObject<CSR>(Certjson);


            // XML File Read
            string Data = File.ReadAllText(path);

            // إعداد الرؤوس
            client.DefaultRequestHeaders.Add("accept", "application/json");
            client.DefaultRequestHeaders.Add("accept-language", "en");
            client.DefaultRequestHeaders.Add("Clearance-Status", "0");
            client.DefaultRequestHeaders.Add("Accept-Version", "V2");
            string rawCredentials = $"{certData.binarySecurityToken}:{certData.secret}";
            string base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(rawCredentials));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/invoices/reporting/single";

            var response = await client.PostAsync(url, content);
            var responseText = await response.Content.ReadAsStringAsync();
            var zatcaResponse = JsonConvert.DeserializeObject<ZATCAResponse>(responseText);
            //MessageBox.Show(response.ReasonPhrase);
            if (zatcaResponse.reportingStatus == "REPORTED")
            {
                _IUW.UBLS.Update(new UBL
                {
                    Id = Ublid,
                    QRCode = qr,
                    Invoicehash = invoiceHash,
                    Uuid = uuid,
                    Invoice = base64Invoice,
                    Saleid = Saleid,
                    Status = "سجلت",
                    Path = path,
                });
                _IUW.Complete();
                MessageBox.Show(zatcaResponse.reportingStatus);
            }
            else
            {
                MessageBox.Show(zatcaResponse.reportingStatus);
            }
            //Console.WriteLine("=================== The End =====================\n");
        }
        public XmlDocument LoadInvoiceFromString(string xmlString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            return doc;
        }
    }
}

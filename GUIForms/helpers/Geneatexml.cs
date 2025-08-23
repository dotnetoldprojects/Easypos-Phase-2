using Domain.Dtos;
using Domain.Models;
using GUI.Helpers;
using GUIForms.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UOW;
using Zatca.EInvoice.SDK;
using Zatca.EInvoice.SDK.Contracts;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace Helpers.Dtos
{
    public class Geneatexml
    {
        Company _Com;
        company DC;
        thirdparty Cust;
        public string Invtitle { get; set; }
        public int Custid { get; set; }
        CsrGenerationDto _CSRDTO;
        IUnitofwork _IUW;
        public Geneatexml()
        {
            _IUW = new Unitofwork(new EasyposEntities());
            DC = _IUW.companies.GetAll().FirstOrDefault();
        }

        public void Createxmldata(List<ProductLine> Pro, company com)
        {
            Cust = new thirdparty();
            Cust = _IUW.thirdparties.GetAll().FirstOrDefault(x => x.ID == Custid);
            _Com = new CompanyProfile().GetCompanyProfile(com.CompanyID, com.ENName, com.Taxnumber, com.CRN);
            var generator = new CsrGenerator();
            _CSRDTO = new CsrGenerationDto(
                commonName: _Com.CommonName,
                serialNumber: _Com.SerialNumber,
                organizationIdentifier: _Com.OrganizationIdentifier,
                organizationUnitName: _Com.OrganizationUnitName,
                organizationName: _Com.Name,
                countryName: _Com.CountryName,
                invoiceType: _Com.InvoiceType,
                locationAddress: _Com.LocationAddress,
                industryBusinessCategory: _Com.IndustryBusinessCategory
            );
            var products = new List<ProductLine>();
            foreach (var p in Pro)
            {
                products.Add(new ProductLine
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                    TaxPercent = p.TaxPercent,
                    Discount = p.Discount
                });
            }
            decimal totalExtension = 0;
            decimal totalTax = 0;
            decimal totalDiscount = 0;
            decimal total = 0;
            decimal totalPrice = 0;
            decimal totalAmount = 0;
            decimal TBV = 0;
            //decimal TAD = 0;

            foreach (var p in products)
            {
                TBV += (p.UnitPrice * p.Quantity)- p.Discount;
                decimal netPrice = p.UnitPrice - p.Discount;
                decimal lineTotal = netPrice * p.Quantity;
                totalPrice += lineTotal;
                decimal taxAmount = Math.Round(lineTotal * p.TaxPercent / 100, 2);

                totalExtension += lineTotal;
                totalTax += taxAmount;
                totalDiscount += p.Discount * p.Quantity;
                total = totalTax + lineTotal;
                //TAD += p.Discount;
                //Console.WriteLine($"Product: {p.Name}, Quantity: {p.Quantity}, Unit Price: {p.UnitPrice}, Line Total: {lineTotal}, Tax Amount: {taxAmount}, Total Amount: {taxAmount + lineTotal}");
            }
            //Console.WriteLine($"Price Amount : {totalPrice}");
            //Console.WriteLine($"Tax Amount : {totalTax}");
            totalAmount = Math.Round(TBV * 15/100, 2)+TBV;
            //Console.WriteLine($"Total : {totalPrice + totalTax}");
            //Console.ReadKey();
            if (_CSRDTO == null)
            {
                throw new InvalidOperationException("❌ CSR Data is missing. تأكد من استدعاء GenerateCSR() أولاً.");
            }
            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false) // 🔥 UTF-8 بدون BOM
            };

            using (var fs = new FileStream("Data/Invoice.xml", FileMode.Create, FileAccess.Write))
            using (var writer = XmlWriter.Create(fs, settings))
            {
                writer.WriteStartDocument();

                // Root element
                writer.WriteStartElement("Invoice", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");

                // Namespaces
                writer.WriteAttributeString("xmlns", "cac", null, "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                writer.WriteAttributeString("xmlns", "cbc", null, "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                writer.WriteAttributeString("xmlns", "ext", null, "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

                // UBLExtensions - جاهز لاستقبال التوقيع لاحقًا
                writer.WriteStartElement("ext", "UBLExtensions", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                writer.WriteStartElement("ext", "UBLExtension", null);
                writer.WriteStartElement("ext", "ExtensionContent", null);
                // التوقيع هينضاف هنا لاحقًا من خلال SDK
                writer.WriteEndElement(); // ExtensionContent
                writer.WriteEndElement(); // UBLExtension
                writer.WriteEndElement(); // UBLExtensions

                // Header
                writer.WriteElementString("cbc", "ProfileID", null, "reporting:1.0");
                writer.WriteElementString("cbc", "ID", null, Invtitle);
                writer.WriteElementString("cbc", "UUID", null, Guid.NewGuid().ToString());
                
                writer.WriteElementString("cbc", "IssueDate", null, DateTime.Now.ToString("yyyy-MM-dd"));
                writer.WriteElementString("cbc", "IssueTime", null, DateTime.Now.ToString("HH:mm:ss"));

                // مثال: 15 أغسطس 2025 الساعة 10:30:45 صباحاً
                //DateTime customDate = new DateTime(2024, 7, 14, 16, 29, 45);

                //writer.WriteElementString("cbc", "IssueDate", null, customDate.ToString("yyyy-MM-dd"));
                //writer.WriteElementString("cbc", "IssueTime", null, customDate.ToString("HH:mm:ss"));

                // Invoice Type Code
                writer.WriteStartElement("cbc", "InvoiceTypeCode", null);
                writer.WriteAttributeString("name", "0100000");
                writer.WriteString("388");
                writer.WriteEndElement();

                writer.WriteElementString("cbc", "DocumentCurrencyCode", null, "SAR");
                writer.WriteElementString("cbc", "TaxCurrencyCode", null, "SAR");

                // ICV
                writer.WriteStartElement("cac", "AdditionalDocumentReference", null);

                writer.WriteStartElement("cbc", "ID", null);
                writer.WriteString("ICV");
                writer.WriteEndElement(); // 👈 نهاية العنصر ID

                writer.WriteStartElement("cbc", "UUID", null);
                writer.WriteString("10");
                writer.WriteEndElement(); // 👈 نهاية العنصر UUID

                writer.WriteEndElement(); // 👈 نهاية العنصر AdditionalDocumentReference

                // Supplier - AccountingSupplierParty
                writer.WriteStartElement("cac", "AccountingSupplierParty", null);
                writer.WriteStartElement("cac", "Party", null);

                // PartyIdentification
                writer.WriteStartElement("cac", "PartyIdentification", null);
                writer.WriteStartElement("cbc", "ID", null);
                writer.WriteAttributeString("schemeID", "CRN");
                writer.WriteString(_Com.CRN);
                writer.WriteEndElement(); // cbc:ID
                writer.WriteEndElement(); // PartyIdentification

                // PostalAddress
                writer.WriteStartElement("cac", "PostalAddress", null);
                writer.WriteElementString("cbc", "StreetName", null, DC.StreetName);
                writer.WriteElementString("cbc", "BuildingNumber", null, "2322");
                writer.WriteElementString("cbc", "CitySubdivisionName", null, DC.CitySubdivisionName);
                writer.WriteElementString("cbc", "CityName", null, DC.CityName);
                writer.WriteElementString("cbc", "PostalZone", null, DC.PostalZone);
                writer.WriteStartElement("cac", "Country", null);
                writer.WriteElementString("cbc", "IdentificationCode", null, "SA");
                writer.WriteEndElement(); // Country
                writer.WriteEndElement(); // PostalAddress

                // PartyTaxScheme
                writer.WriteStartElement("cac", "PartyTaxScheme", null);
                writer.WriteElementString("cbc", "CompanyID", null, _CSRDTO.OrganizationIdentifier);
                writer.WriteStartElement("cac", "TaxScheme", null);
                writer.WriteElementString("cbc", "ID", null, "VAT");
                writer.WriteEndElement(); // TaxScheme
                writer.WriteEndElement(); // PartyTaxScheme

                // PartyLegalEntity
                writer.WriteStartElement("cac", "PartyLegalEntity", null);
                writer.WriteElementString("cbc", "RegistrationName", null, _CSRDTO.OrganizationName);
                writer.WriteEndElement(); // PartyLegalEntity

                writer.WriteEndElement(); // Party
                writer.WriteEndElement(); // AccountingSupplierParty

                // Customer - AccountingCustomerParty

                writer.WriteStartElement("cac", "AccountingCustomerParty", null);
                writer.WriteStartElement("cac", "Party", null);

                // PartyIdentification
                writer.WriteStartElement("cac", "PartyIdentification", null);
                writer.WriteStartElement("cbc", "ID", null);
                writer.WriteAttributeString("schemeID", "CRN");
                writer.WriteString(Cust.CRN);
                writer.WriteEndElement(); // cbc:ID
                writer.WriteEndElement(); // PartyIdentification

                // PostalAddress
                writer.WriteStartElement("cac", "PostalAddress", null);
                writer.WriteElementString("cbc", "StreetName", null, Cust.StreetName);
                writer.WriteElementString("cbc", "BuildingNumber", null, Cust.Billnumber);
                writer.WriteElementString("cbc", "CitySubdivisionName", null, Cust.CitySubdivisionName);
                writer.WriteElementString("cbc", "CityName", null, Cust.CityName);
                writer.WriteElementString("cbc", "PostalZone", null, Cust.PostalZone);
                writer.WriteStartElement("cac", "Country", null);
                writer.WriteElementString("cbc", "IdentificationCode", null, "SA");
                writer.WriteEndElement(); // Country
                writer.WriteEndElement(); // PostalAddress

                // PartyTaxScheme
                writer.WriteStartElement("cac", "PartyTaxScheme", null);
                writer.WriteElementString("cbc", "CompanyID", null, Cust.Taxnumber);
                writer.WriteStartElement("cac", "TaxScheme", null);
                writer.WriteElementString("cbc", "ID", null, "VAT");
                writer.WriteEndElement(); // TaxScheme
                writer.WriteEndElement(); // PartyTaxScheme

                // PartyLegalEntity
                writer.WriteStartElement("cac", "PartyLegalEntity", null);
                writer.WriteElementString("cbc", "RegistrationName", null, Cust.Taxnumber);
                writer.WriteEndElement(); // PartyLegalEntity

                writer.WriteEndElement(); // Party
                writer.WriteEndElement(); // AccountingCustomerParty

                // Delivery
                writer.WriteStartElement("cac", "Delivery", null);
                writer.WriteElementString("cbc", "ActualDeliveryDate", null, DateTime.Now.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                //PaymentMeans
                writer.WriteStartElement("cac", "PaymentMeans", null);
                writer.WriteElementString("cbc", "PaymentMeansCode", null, "97");
                writer.WriteEndElement();

                writer.WriteStartElement("cac", "AllowanceCharge", null);

                writer.WriteElementString("cbc", "ChargeIndicator", null, "false");
                writer.WriteElementString("cbc", "AllowanceChargeReason", null, "discount");

                // Amount with currency attribute
                writer.WriteStartElement("cbc", "Amount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString("0.00");
                writer.WriteEndElement(); // Amount

                // TaxCategory
                writer.WriteStartElement("cac", "TaxCategory", null);

                // ID with attributes
                writer.WriteStartElement("cbc", "ID", null);
                writer.WriteAttributeString("schemeID", "UN/ECE 5305");
                writer.WriteAttributeString("schemeAgencyID", "6");
                writer.WriteString("S");
                writer.WriteEndElement(); // ID

                writer.WriteElementString("cbc", "Percent", null, "15");

                // TaxScheme
                writer.WriteStartElement("cac", "TaxScheme", null);
                writer.WriteStartElement("cbc", "ID", null);
                writer.WriteAttributeString("schemeID", "UN/ECE 5153");
                writer.WriteAttributeString("schemeAgencyID", "6");
                writer.WriteString("VAT");
                writer.WriteEndElement(); // ID inside TaxScheme
                writer.WriteEndElement(); // TaxScheme

                writer.WriteEndElement(); // TaxCategory
                writer.WriteEndElement(); // AllowanceCharge


                // Tax Total
                writer.WriteStartElement("cac", "TaxTotal", null);
                writer.WriteStartElement("cbc", "TaxAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString(totalTax.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement("cac", "TaxTotal", null);
                writer.WriteStartElement("cbc", "TaxAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString(totalTax.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("cac", "TaxSubtotal", null);
                writer.WriteStartElement("cbc", "TaxableAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString(totalPrice.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("cbc", "TaxAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString(totalTax.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("cac", "TaxCategory", null);
                writer.WriteStartElement("cbc", "ID", null);
                writer.WriteAttributeString("schemeID", "UN/ECE 5305");
                writer.WriteAttributeString("schemeAgencyID", "6");
                writer.WriteString("S");
                writer.WriteEndElement();

                writer.WriteElementString("cbc", "Percent", null, "15.00");
                writer.WriteStartElement("cac", "TaxScheme", null);
                writer.WriteStartElement("cbc", "ID", null);
                writer.WriteAttributeString("schemeID", "UN/ECE 5153");
                writer.WriteAttributeString("schemeAgencyID", "6");
                writer.WriteString("VAT");
                writer.WriteEndElement();

                writer.WriteEndElement(); // TaxScheme
                writer.WriteEndElement(); // TaxCategory
                writer.WriteEndElement(); // TaxSubtotal
                writer.WriteEndElement(); // TaxTotal

                // Legal Monetary Total
                writer.WriteStartElement("cac", "LegalMonetaryTotal", null);

                writer.WriteStartElement("cbc", "LineExtensionAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString(TBV.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("cbc", "TaxExclusiveAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString(TBV.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("cbc", "TaxInclusiveAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString(totalAmount.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("cbc", "AllowanceTotalAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString("0.00");
                writer.WriteEndElement();

                writer.WriteStartElement("cbc", "PrepaidAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString("0.00");
                writer.WriteEndElement();

                writer.WriteStartElement("cbc", "PayableAmount", null);
                writer.WriteAttributeString("currencyID", "SAR");
                writer.WriteString(totalAmount.ToString());
                writer.WriteEndElement();

                writer.WriteEndElement(); // LegalMonetaryTotal
                foreach (var p in products)
                {
                    decimal netPrice = p.UnitPrice - p.Discount;
                    decimal lineTotal = netPrice * p.Quantity;
                    decimal taxAmount = Math.Round(lineTotal * p.TaxPercent / 100, 2);
                    // Invoice Line
                    writer.WriteStartElement("cac", "InvoiceLine", null);

                    writer.WriteElementString("cbc", "ID", null, p.Id);

                    writer.WriteStartElement("cbc", "InvoicedQuantity", null);
                    writer.WriteAttributeString("unitCode", "PCE");
                    writer.WriteString(p.Quantity.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("cbc", "LineExtensionAmount", null);
                    writer.WriteAttributeString("currencyID", "SAR");
                    writer.WriteString(lineTotal.ToString());
                    writer.WriteEndElement();

                    // TaxTotal
                    writer.WriteStartElement("cac", "TaxTotal", null);
                    writer.WriteStartElement("cbc", "TaxAmount", null);
                    writer.WriteAttributeString("currencyID", "SAR");
                    writer.WriteString(taxAmount.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("cbc", "RoundingAmount", null);
                    writer.WriteAttributeString("currencyID", "SAR");
                    writer.WriteString((taxAmount + lineTotal).ToString());
                    writer.WriteEndElement();
                    writer.WriteEndElement(); // TaxTotal

                    // Item
                    writer.WriteStartElement("cac", "Item", null);
                    writer.WriteElementString("cbc", "Name", null, p.Name);

                    // ClassifiedTaxCategory
                    writer.WriteStartElement("cac", "ClassifiedTaxCategory", null);
                    writer.WriteElementString("cbc", "ID", null, "S");
                    writer.WriteElementString("cbc", "Percent", null, "15.00");

                    // TaxScheme
                    writer.WriteStartElement("cac", "TaxScheme", null);
                    writer.WriteElementString("cbc", "ID", null, "VAT");
                    writer.WriteEndElement(); // TaxScheme

                    writer.WriteEndElement(); // ClassifiedTaxCategory
                    writer.WriteEndElement(); // Item

                    // Price
                    writer.WriteStartElement("cac", "Price", null);
                    writer.WriteStartElement("cbc", "PriceAmount", null);
                    writer.WriteAttributeString("currencyID", "SAR");
                    writer.WriteString(p.UnitPrice.ToString());
                    writer.WriteEndElement();

                    // AllowanceCharge inside Price
                    writer.WriteStartElement("cac", "AllowanceCharge", null);
                    writer.WriteElementString("cbc", "ChargeIndicator", null, "false");
                    writer.WriteElementString("cbc", "AllowanceChargeReason", null, "discount");

                    writer.WriteStartElement("cbc", "Amount", null);
                    writer.WriteAttributeString("currencyID", "SAR");
                    writer.WriteString(p.Discount.ToString());
                    writer.WriteEndElement(); // Amount

                    writer.WriteEndElement(); // AllowanceCharge
                    writer.WriteEndElement(); // Price

                    writer.WriteEndElement(); // InvoiceLine
                }
                writer.WriteEndElement(); // Invoice
                writer.WriteEndDocument();

            }
            //MessageBox.Show("✅ The invoice has been created .\n");
            //Console.ReadKey();
            //MessageBox.Show("✅ Invoice XML saved to: " + "Data/Invoice.xml\n");
            //Console.ReadKey();
            //return sb.ToString();
        }
    }
}

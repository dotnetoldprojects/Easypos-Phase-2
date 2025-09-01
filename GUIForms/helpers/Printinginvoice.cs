using Aspose.Pdf;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer;
using Domain;
using Domain.Models;
using GUIForms.Dtos;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Filespec;
using iText.Layout;
using jdk.@internal.util.xml.impl;
using QRCoder;
using Reporting;
using Reporting.sales.Normal.Big;
using Reporting.sales.Normal.Small;
using Reporting.tailor;
using Reporting.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;

namespace GUIForms.helpers
{
    public class Printinginvoice
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        public string Filename { get; set; }
        public string Purchaseorder { get; set; }
        public string RN { get; set; }
        public string Proname { get; set; }
        public string Custaddress { get; set; }
        public string CustTaxnumber { get; set; }
        public string Totalafterdiscount { get; set; }
        public string Wordofnumber { get; set; }
        public Printinginvoice()
        {
            GC = new Getcentralaizes();
            _IUW = new Unitofwork(new EasyposEntities());
            DC = (company)GC.Getcompanydatalist();
        }
        public void Invoice(int Invid)
        {
            ReportDocument Rep = new ReportDocument();
            Frmreporting FR = new Frmreporting();
            if (DC.Sysnametype == "طباعة نظام مبيعات صغير خياطه")
            {
                if (DC.ISUsePhase2)
                {
                    Rep = new SmallInv();
                }
                else
                {
                    Rep = new Smot();
                }
            }
            else if (DC.Sysnametype == "طباعة نظام مبيعات كبير")
            {
                Rep = new Bigsalesbill();
            }
            else
            {
                Rep = new Smallsalesbill();
            }
            Dataset Ds = new Dataset();
            string date = "";
            string Paied = "";
            string Dept = "";
            if (Invid != 0)
            {
                var result = (from s in _IUW.sales.GetAll()
                              join sd in _IUW.salesdetailes.GetAll()
                                  on s.Invoiceno equals sd.InvoiceNo into salesDetailsJoin
                              from sd in salesDetailsJoin.DefaultIfEmpty()

                              join tp in _IUW.thirdparties.GetAll()
                                  on s.ThirdPartyID equals tp.ID into thirdPartyJoin
                              from tp in thirdPartyJoin.DefaultIfEmpty()

                              join pay in _IUW.payments.GetAll()
                                  on s.Invoiceno equals pay.InvoiceNo into paymentJoin
                              from pay in paymentJoin.DefaultIfEmpty()

                              join ubl in _IUW.UBLS.GetAll()
                                  on s.Invoiceno equals ubl.Saleid into ublJoin
                              from ubl in ublJoin.DefaultIfEmpty()

                              where s.Invoiceno == Invid

                              select new
                              {
                                  s.Invoiceno,
                                  s.TDate,
                                  s.TTime,
                                  s.NonVatTotal,
                                  s.Discount,
                                  s.VatAmount,
                                  s.TotalAmount,
                                  ThirdPartyName = tp != null ? tp.Name : null,
                                  MobileNumber = tp != null ? tp.MobileNumber : null,
                                  TaxNumber = tp != null ? tp.Taxnumber : null,
                                  Custaddress = tp != null ? tp.Address : null,
                                  s.Billtype,
                                  s.Note,
                                  TDDesc = sd != null ? sd.TDDesc : null,
                                  Quantity = sd != null ? sd.Quantity : (double?)null,
                                  Subtotal = sd != null ? sd.Subtotal : (double?)null,
                                  Total = sd != null ? sd.Total : (decimal?)null,
                                  QRCode = ubl != null ? ubl.QRCode : null,
                                  Path = ubl != null ? ubl.Path : null,
                                  Purchaseorder = s.Purchaseorder,
                                  RN = s.RN,
                                  Proname = s.Proname,
                                  pay.Paid,
                                  pay.Remaining
                              }).ToList();
                foreach (var item in result)
                {
                    Paied = item.Paid.ToString();
                    Dept = item.Remaining.ToString();
                    Filename = item.Path;
                    date = item.TDate;
                    var qrText = item.QRCode;
                    byte[] QRCode = null;
                    Purchaseorder = item.Purchaseorder;
                    RN = item.RN;
                    Proname = item.Proname;
                    Custaddress = item.Custaddress;
                    CustTaxnumber = item.TaxNumber;
                    Totalafterdiscount = item.NonVatTotal.ToString();
                    if (!string.IsNullOrEmpty(qrText))
                    {
                        using (var qrGenerator = new QRCodeGenerator())
                        using (var qrData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q))
                        using (var qrCode = new QRCode(qrData))
                        using (var bitmap = qrCode.GetGraphic(20)) // 20 هو الحجم
                        using (var stream = new MemoryStream())
                        {
                            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            QRCode = stream.ToArray();
                        }
                    }
                    //byte[] QRCode = Convert.FromBase64String(item.QRCode);
                    byte[] CLogo = Convert.FromBase64String(DC.CompanyLogo);
                    var Tot = Convert.ToDecimal(item.TotalAmount);
                    decimal GTot = Convert.ToDecimal(string.Format("{0:0.00}", Tot));
                    ConvertNumbersToArabicAlphabet a = new ConvertNumbersToArabicAlphabet(GTot.ToString());
                    Wordofnumber = a.GetNumberAr();
                    Ds.Bill.Rows.Add(new object[] {
                        item.Invoiceno,
                        item.NonVatTotal,
                        item.Quantity,
                        0,
                        item.Total,
                        item.ThirdPartyName,
                        QRCode,
                        null,
                        item.TDDesc,
                        null,
                        item.VatAmount,
                        item.TotalAmount,
                        CLogo,
                        DC.PhoneNo,
                        item.Note
                    });
                }
                Rep.SetDataSource(Ds);


                FR.CRV.ReportSource = Rep;
                FR.CRV.Refresh();

                Rep.SetParameterValue("CompanyName", DC.Name);
                Rep.SetParameterValue("Address", DC.Address);
                Rep.SetParameterValue("Taxnum", DC.Taxnumber);
                Rep.SetParameterValue("Date", date);
                if (DC.Sysnametype == "طباعة نظام مبيعات صغير خياطه")
                {
                    Rep.SetParameterValue("DD", "0");
                    Rep.SetParameterValue("RD", "0");
                    Rep.SetParameterValue("Discount", "0");
                    Rep.SetParameterValue("Tax", "0");
                    Rep.SetParameterValue("Paied", Paied);
                    Rep.SetParameterValue("Dept", Dept);
                    Rep.SetParameterValue("PhoneNo", DC.PhoneNo);
                    //Save As PDF
                    var invname = Filename + ".pdf";
                    var PD = Directory.GetCurrentDirectory() + @"\Data\";
                    var Pathdata = Directory.GetCurrentDirectory() + @"\Data\";
                    if (!Directory.Exists(Pathdata))
                    {
                        Directory.CreateDirectory(Pathdata);
                    }
                    Pathdata += invname;
                    FR.Show();
                    //Rep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Pathdata);
                }
                if (DC.Sysnametype != "طباعة نظام مبيعات كبير")
                {
                    Rep.SetParameterValue("Shopname", DC.Name);
                    Rep.SetParameterValue("PhoneNo", DC.PhoneNo);
                    Rep.SetParameterValue("Paied", Paied);
                    Rep.SetParameterValue("Dept", Dept);
                    FR.Show();
                    //Rep.PrintOptions.PrinterName = DC.Printername;
                    //Rep.PrintToPrinter(1, true, 1, 1);
                }
                if (DC.Sysnametype == "طباعة نظام مبيعات كبير")
                {
                    Rep.SetParameterValue("English_Shop_name", DC.ENName);
                    Rep.SetParameterValue("Cashirname", "Admin");
                    //تفقيط الارقام
                    Rep.SetParameterValue("Wordofnumber", Wordofnumber);
                    Rep.SetParameterValue("Projectname", Proname ?? "مشروع");
                    Rep.SetParameterValue("Refransenumber", RN ?? "Refranse number");
                    Rep.SetParameterValue("Custaddress", Custaddress ?? "السعوديه" );
                    Rep.SetParameterValue("Custtax", CustTaxnumber ?? "3000000000000003" );
                    Rep.SetParameterValue("Paymethode", "مختلط" );
                    Rep.SetParameterValue("Totaldiscount", "0" );
                    Rep.SetParameterValue("Totalafterdiscount", Totalafterdiscount);
                    Rep.SetParameterValue("Proname", DC.CRN);
                    FR.Show();
                }
            }
            else
            {
                MessageBox.Show("لا يوجد فاتورة لطباعتها", "خطأ");
            }
        }
    }
}

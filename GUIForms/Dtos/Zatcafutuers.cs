using Aspose.Pdf;
using Domain.Dtos;
using Domain.Models;
using GUI.Helpers;
using GUIForms.helpers;
using Helpers.Dtos;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using UOW;

namespace GUIForms.Dtos
{
    public class Zatcafutuers
    {
        IUnitofwork _IUW;
        public int invid { get; set; }
        public int nextNumber { get; set; }
        public string Zatcainv { get; set; }
        public company DC { get; set; }
        Getcentralaizes GC;
        public Zatcafutuers()
        {
            if (invid > 0) {
                Loading();
            }
        }
        public void Loading()
        {
            _IUW = new Unitofwork(new EasyposEntities());
            Getzatcaid();      
        }
        private void Getzatcaid()
        {
            var lastInvoice = _IUW.UBLS.GetAll().OrderByDescending(i => i.Saleid).FirstOrDefault();
            nextNumber = 1;
            if (lastInvoice != null)
            {
                nextNumber = (lastInvoice?.Saleid ?? 0) + 1;
            }
            // Format the new invoice number
            Zatcainv = $"inv-{nextNumber.ToString("D5")}";
            Getsalesdata();
        }
        private void Getsalesdata()
        {
            var Salesinnvoice = _IUW.sales.Get(invid);
            var SDinvoice = _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == invid).ToList();
            Generatexml(Salesinnvoice, SDinvoice);
        }
        private async void Generatexml(sale sal,  List<salesdetaile> SD)
        {
            List<ProductLine> productLines = new List<ProductLine>();
            Geneatexml GXL = new Geneatexml();
            GXL.Custid = (int)sal.ThirdPartyID;
            GXL.Invtitle = Zatcainv;
            const string unitCode = "PCE";
            const decimal taxPercent = 15m;

            for (int i = 0; i < SD.Count; i++)
            {
                productLines.Add(new ProductLine
                {
                    Id = SD[i].TDetailNo.ToString(),
                    Name = SD[i].TDDesc.ToString(),
                    Quantity = int.Parse(SD[i].Quantity.ToString()),
                    UnitCode = unitCode,
                    UnitPrice = decimal.Parse(SD[i].ItemPrice.ToString()),
                    Discount = decimal.Parse(SD[i].Discount.ToString()),
                    TaxPercent = taxPercent
                });
            }
            string InputPath = @"Data/Invoice.xml";
            var data = DC;
            var RBD = Convert.ToDecimal(sal.Discount);
            bool RB2 = false;
            if (RBD > 0) {
                RB2 = true;
            }
            GXL.Createxmldata(productLines, DC, RB2, RBD);

            var xmlContent = File.ReadAllText(InputPath);
            GC = new Getcentralaizes();
            var Doc = GC.LoadInvoiceFromString(xmlContent);
            Signdtos Sdtos = new Signdtos();
            Sdtos.Saleid = nextNumber;
            await Sdtos.Sign(Doc, Zatcainv);


            var GUL = _IUW.UBLS.GetAll().Where(x => x.Saleid == Sdtos.Saleid).FirstOrDefault();
            Sdtos.Ublid = GUL.Id;
            await Sdtos.SendInvoiceAsync(GUL.Invoicehash, GUL.Uuid, GUL.Invoice, GUL.Path, GUL.QRCode);
        }
    }
}

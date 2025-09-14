using Aspose.Pdf;
using Domain.Dtos;
using Domain.Models;
using GUI.Helpers;
using GUIForms.helpers;
using Helpers.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            // Constructor فارغ، التحميل بيتم يدويًا
        }

        public async Task Loading()
        {
            _IUW = new Unitofwork(new EasyposEntities());
            await Getzatcaid();
        }

        private async Task Getzatcaid()
        {
            var lastInvoice = _IUW.UBLS.GetAll().OrderByDescending(i => i.Saleid).FirstOrDefault();
            nextNumber = (int)(lastInvoice != null ? (lastInvoice.Saleid + 1) : 1);
            Zatcainv = $"inv-{nextNumber:D5}";
            await Getsalesdata();
        }

        private async Task Getsalesdata()
        {
            var Salesinnvoice = _IUW.sales.Get(invid);
            var SDinvoice = _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == invid).ToList();
            await Generatexml(Salesinnvoice, SDinvoice);
        }

        private async Task Generatexml(sale sal, List<salesdetaile> SD)
        {
            List<ProductLine> productLines = new List<ProductLine>();
            Geneatexml GXL = new Geneatexml
            {
                Custid = (int)sal.ThirdPartyID,
                Invtitle = Zatcainv
            };

            const string unitCode = "PCE";
            const decimal taxPercent = 15m;

            foreach (var item in SD)
            {
                productLines.Add(new ProductLine
                {
                    Id = item.TDetailNo.ToString(),
                    Name = item.TDDesc,
                    Quantity = int.Parse(item.Quantity.ToString()),
                    UnitCode = unitCode,
                    UnitPrice = decimal.Parse(item.ItemPrice.ToString()),
                    Discount = decimal.Parse(item.Discount.ToString()),
                    TaxPercent = taxPercent
                });
            }

            string InputPath = @"Data/Invoice.xml";
            var RBD = Convert.ToDecimal(sal.Discount);
            bool RB2 = RBD > 0;

            GXL.Createxmldata(productLines, DC, RB2, RBD);

            var xmlContent = File.ReadAllText(InputPath);
            GC = new Getcentralaizes();
            var Doc = GC.LoadInvoiceFromString(xmlContent);

            Signdtos Sdtos = new Signdtos
            {
                Saleid = nextNumber,
                invno = invid
            };

            await Sdtos.Sign(Doc, Zatcainv);

            var GUL = _IUW.UBLS.GetAll().FirstOrDefault(x => x.Saleid == Sdtos.Saleid);
            if (GUL != null)
            {
                Sdtos.Ublid = GUL.Id;
                await Sdtos.SendInvoiceAsync(GUL.Invoicehash, GUL.Uuid, GUL.Invoice, GUL.Path, GUL.QRCode);
            }
        }
    }
}

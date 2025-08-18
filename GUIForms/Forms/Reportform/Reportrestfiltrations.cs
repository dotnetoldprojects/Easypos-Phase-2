using Centeralized;
using CrystalDecisions.CrystalReports.Engine;
using Domain.Models;
using GUIForms.Dtos;
using MetroFramework.Forms;
using Org.BouncyCastle.Asn1.X500;
using Reporting;
using Reporting.resturantreports;
using sun.misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;
using Dataset = Centeralized.Dataset;

namespace Resturantlayer
{
    public partial class Reportrestfiltrations : MetroForm
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        Dataset Ds;
        ReportDocument RD;
        public Reportrestfiltrations()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            Ds = new Dataset();
            var DS = DTF.Value.ToString("yyyy-MM-dd");
            var De = DTT.Value.AddDays(1).ToString("yyyy-MM-dd");
            DTF.Text = DS;
            DTT.Text = De;
            var TS = TTF.Value.ToString("12:00:00");
            TTF.Text = TS;
            var TE = TTF.Value.AddHours(14);
            TTE.Text = TE.ToString();
            RD = new ReportDocument();
        }
        private void Btnshowreport_Click(object sender, EventArgs e)
        {
            Frmreporting FR = new Frmreporting();
            Ds = new Dataset();
            DateTime fromDateTime = DateTime.Parse($"{DTF.Value:yyyy-MM-dd} {TTF.Value:HH:mm:ss}");
            DateTime toDateTime = DateTime.Parse($"{DTT.Value:yyyy-MM-dd} {TTE.Value:HH:mm:ss}");
            if (RBAllsales.Checked)
            {
                RD = new Summarysales();
                // 1- جدول المبيعات (dtbasic)
                var dtbasic = (from sd in _IUW.salesdetailes.GetAll()
                               join s in _IUW.sales.GetAll() on sd.InvoiceNo equals s.Invoiceno
                               let saleDateTime = DateTime.Parse($"{s.TDate:yyyy-MM-dd} {s.TTime:HH:mm:ss}")
                               where saleDateTime >= fromDateTime && saleDateTime <= toDateTime
                               select new
                               {
                                   s.Invoiceno,
                                   s.TDate,
                                   sd.ProductNo,
                                   sd.TDDesc,
                                   sd.Quantity,
                                   sd.ItemPrice,
                                   Total = sd.Quantity * sd.ItemPrice,
                                   sd.Discount,
                               }).ToList();

                DataTable dt = Ds.Tables["dtbasic"];
                foreach (var item in dtbasic)
                {
                    DataRow row = dt.NewRow();
                    row["ProductNo"] = item.ProductNo;
                    row["TDDesc"] = item.TDDesc;
                    row["Quantity"] = item.Quantity;
                    row["Price"] = item.ItemPrice;
                    row["Subtotal"] = item.Total;
                    row["Discount"] = item.Discount;
                    row["Totafterdiscount"] = item.Total;
                    dt.Rows.Add(row);
                }


                // 2- جدول المدفوعات (Dtpay)
                var dtpay = (from p in _IUW.payments.GetAll()
                             join s in _IUW.sales.GetAll() on p.InvoiceNo equals s.Invoiceno
                             let saleDateTime = DateTime.Parse($"{s.TDate:yyyy-MM-dd} {s.TTime:HH:mm:ss}")
                             where saleDateTime >= fromDateTime && saleDateTime <= toDateTime
                             select new
                             {
                                 s.Invoiceno,
                                 p.Date,
                                 p.PaymentMethod,
                                 p.Paid
                             }).ToList();

                // 4- تفاصيل الفاتورة (invoicedetailes)
                var invoicedetailes = (from sd in _IUW.salesdetailes.GetAll()
                                       join s in _IUW.sales.GetAll() on sd.InvoiceNo equals s.Invoiceno
                                       let saleDateTime = DateTime.Parse($"{s.TDate:yyyy-MM-dd} {s.TTime:HH:mm:ss}")
                                       where saleDateTime >= fromDateTime && saleDateTime <= toDateTime
                                       join t in _IUW.thirdparties.GetAll() on s.ThirdPartyID equals t.ID
                                       join p in _IUW.payments.GetAll() on s.Invoiceno equals p.InvoiceNo into payJoin
                                       from pj in payJoin.DefaultIfEmpty()
                                       select new
                                       {
                                           s.Discount,
                                           sd.ProductNo,
                                           sd.TDDesc,
                                           sd.ItemPrice,
                                           sd.Quantity,
                                           sd.Total,
                                           s.Invoiceno,
                                           s.TotalAmount,
                                           s.TDate,
                                           CustomerName = t.Name,
                                           PaymentAmount = pj != null ? pj.Paid : 0
                                       }).ToList();

                DataTable dt2 = Ds.Tables["invoicedetailes"];
                foreach (var item in invoicedetailes)
                {
                    DataRow row = dt2.NewRow();
                    row["Discount"] = item.Discount;
                    row["productnumber"] = item.ProductNo;
                    row["description"] = item.TDDesc;
                    row["price"] = item.ItemPrice;
                    row["quantity"] = item.Quantity;
                    row["total"] = item.Total;
                    row["invoiceid"] = item.Invoiceno;
                    row["totaldet"] = item.TotalAmount;
                    row["customername"] = item.CustomerName;
                    row["ReceivedAmount"] = item.PaymentAmount;
                    dt2.Rows.Add(row);
                }

                // 3- جدول العمليات البنكية (Banktrans)
                //var banktrans = _IUW.banktransactions.GetAll()
                //    .Select(b => new
                //    {
                //        b.TransactionId,
                //        b.BankName,
                //        b.TransactionDate,
                //        b.Amount
                //    }).ToList();

                //ds.Tables.Add(ToDataTable(banktrans, "Banktrans"));

                // ربط الـ DataSet مع التقرير
                //RD.SetDataSource(ds);
            }
            else if (RBItemsales.Checked)
            {
                RD = new Summaryitems();
                var sales = _IUW.sales.GetAll().ToList();
                var salesdetailes = _IUW.salesdetailes.GetAll().ToList();
                var productitems = _IUW.productitems.GetAll().ToList();
                var items = _IUW.items.GetAll().ToList();
                var unittypes = _IUW.unittypes.GetAll().ToList();
                var query = from s in sales
                            let saleDateTime = DateTime.Parse($"{s.TDate:yyyy-MM-dd} {s.TTime:HH:mm:ss}")
                            where saleDateTime >= fromDateTime && saleDateTime <= toDateTime
                            join sd in salesdetailes on s.Invoiceno equals sd.InvoiceNo into sdGroup
                            from sd in sdGroup.DefaultIfEmpty()
                            join pi in productitems on sd?.ProductNo.ToString() equals pi?.Proid into piGroup
                            from pi in piGroup.DefaultIfEmpty()
                            join it in items on pi?.itemid equals it?.ID.ToString() into itGroup
                            from it in itGroup.DefaultIfEmpty()
                            join u in unittypes on it?.UID equals u?.ID into uGroup
                            from u in uGroup.DefaultIfEmpty()
                            where it?.Itemname != null
                            group new { pi, it, u } by new
                            {
                                ID = it?.ID,
                                Itemname = it?.Itemname,
                                UName = u?.UName,
                                ItemQty = (decimal?)it?.Itemqty,
                                ItemPrice = (decimal?)it?.Itemprice
                            } into g
                            select new
                            {
                                ID = g.Key.ID,
                                Itemname = g.Key.Itemname,
                                UName = g.Key.UName,
                                ItemQty = g.Key.ItemQty,
                                ItemPrice = g.Key.ItemPrice,
                                Quantity = g.Sum(x => decimal.Parse(x.pi?.Quantity)),
                                Total = g.Sum(x => decimal.Parse(x.pi?.Quantity) * (g.Key.ItemPrice ?? 0)),
                                QBD = g.Sum(x => decimal.Parse(x.pi?.Quantity) + (g.Key.ItemQty ?? 0))
                            };

                var result = query.OrderBy(x => x.ID).ToList();


                DataTable dt = Ds.Tables["productitems"];
                foreach (var item in result)
                {
                    DataRow row = dt.NewRow();
                    row["Itemname"] = item.Itemname;
                    row["UnitType"] = item.UName;
                    row["itemqty"] = item.ItemQty;
                    row["Quantity"] = item.Quantity;
                    row["Itemprice"] = item.ItemPrice;
                    row["Total"] = item.Total;
                    row["QBD"] = item.QBD;
                    dt.Rows.Add(row);
                }
            }
            else
            {
                MessageBox.Show("برجاء اختيار نوع التقرير", "خطأ");
                return;
            }
            RD.SetDataSource(Ds);
            RD.SetParameterValue("SalesDate", $"من {DTF.Value.ToString("dd-MM-yyyy")} " + " " + $" إلي {DTT.Value.ToString("dd-MM-yyyy")}");
            RD.SetParameterValue("English_Shop_name", DC.ENName);
            RD.SetParameterValue("CompanyName", DC.Name);
            FR.CRV.ReportSource = RD;
            FR.Show();
        }
    }
}

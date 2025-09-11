using CrystalDecisions.CrystalReports.Engine;
using Domain;
using Domain.Models;
using GUIForms.Dtos;
using MetroFramework.Forms;
using Org.BouncyCastle.Asn1.X500;
using Reporting;
using Reporting.resturantreports;
using Reporting.VM;
using sun.misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;

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
            Dataset Ds = new Dataset();

            // محاولة تحويل التاريخ والوقت مع التحقق
            if (!DateTime.TryParse($"{DTF.Value:yyyy-MM-dd} {TTF.Value:HH:mm:ss}", out DateTime fromDateTime) ||
                !DateTime.TryParse($"{DTT.Value:yyyy-MM-dd} {TTE.Value:HH:mm:ss}", out DateTime toDateTime))
            {
                MessageBox.Show("تاريخ أو وقت غير صالح", "خطأ");
                return;
            }

            if (RBAllsales.Checked)
            {
                RD = new Summarysales();

                // تجهيز الجداول داخل الـ DataSet
                if (!Ds.Tables.Contains("dtbasic"))
                    Ds.Tables.Add(new DataTable("dtbasic"));
                if (!Ds.Tables.Contains("invoicedetailes"))
                    Ds.Tables.Add(new DataTable("invoicedetailes"));

                var sales = _IUW.sales.GetAll().ToList();
                var salesdetailes = _IUW.salesdetailes.GetAll().ToList();
                var payments = _IUW.payments.GetAll().ToList();
                var thirdparties = _IUW.thirdparties.GetAll().ToList();

                // جدول المبيعات
                var query = from d in salesdetailes
                            join s in sales on d.InvoiceNo equals s.Invoiceno into gj
                            from s in gj.DefaultIfEmpty() // Left Outer Join
                            let saleDateTime = DateTime.TryParse($"{s.TDate} {s.TTime}", out var dtt) ? dtt : (DateTime?)null
                            where saleDateTime != null && saleDateTime >= fromDateTime && saleDateTime <= toDateTime
                            group new { d, s } by new { d.ProductNo, d.TDDesc, d.ItemPrice } into g
                            orderby g.Key.ProductNo
                            select new
                            {
                                ProductNo = g.Key.ProductNo,
                                TDDesc = g.Key.TDDesc,
                                Price = g.Key.ItemPrice,
                                Quantity = g.Sum(x => x.d.Quantity),
                                Subtotal = g.Sum(x => x.d.Subtotal),
                                Discount = g.Sum(x => x.d.Discount),
                                Totafterdiscount = g.Sum(x => x.d.Totafterdiscount)
                            };

                var result = query.ToList();
                var dt = Ds.Tables["dtbasic"];
                foreach (var item in result)
                {
                    var row = dt.NewRow();
                    row["ProductNo"] = item.ProductNo;
                    row["TDDesc"] = item.TDDesc;
                    row["Quantity"] = item.Quantity;
                    row["Price"] = item.Price;
                    row["Subtotal"] = item.Subtotal;
                    row["Discount"] = item.Discount;
                    row["Totafterdiscount"] = item.Subtotal - item.Discount;
                    dt.Rows.Add(row);
                }


                //var filteredPayments = (from pay in payments
                //                        join sa in sales
                //                            on pay.InvoiceNo equals sa.Invoiceno
                //                        let saleDateTime = DateTime.TryParse(sa.TDate + " " + sa.TTime, out var dtt) ? dtt : (DateTime?)null
                //                        select new
                //                        {
                //                            SaleDateTime = saleDateTime,
                //                            PChange = pay.Remaining,
                //                            Paid = pay.Cash,
                //                            Bank = pay.Bank
                //                        }).ToList();
                var filteredPayments = (from pay in payments
                                        join sa in sales
                                            on pay.InvoiceNo equals sa.Invoiceno into salesGroup
                                        from sa in salesGroup.DefaultIfEmpty()
                                        let saleDateTime = DateTime.TryParse(pay.Date + " " + pay.Time, out var dtt) ? dtt : (DateTime?)null
                                        select new
                                        {
                                            SaleDateTime = saleDateTime,
                                            PChange = pay.Remaining,
                                            Paid = pay.Cash,
                                            Bank = pay.Bank
                                        }).ToList();



                // فلترة حسب النطاق الزمني
                var FP = filteredPayments
                            .Where(x => x.SaleDateTime.HasValue &&
                                        x.SaleDateTime.Value >= fromDateTime &&
                                        x.SaleDateTime.Value <= toDateTime)
                            .ToList();


                // مثال: جمع المجاميع بعد الفلترة
                var totalPChange = FP.Sum(x => x.PChange);
                var totalPaid = FP.Sum(x => x.Paid);
                var totalBank = FP.Sum(x => x.Bank);
                // إضافة النتائج للـ DataTable داخل DataSet
                if (!Ds.Tables.Contains("Dtpay"))
                {
                    var dt2 = new DataTable("Dtpay");
                    dt2.Columns.Add("PChange", typeof(decimal));
                    dt2.Columns.Add("Paid", typeof(decimal));
                    dt2.Columns.Add("Bank", typeof(decimal));
                    Ds.Tables.Add(dt2);
                }

                Ds.Tables["Dtpay"].Clear();
                Ds.Tables["Dtpay"].Rows.Add(totalPChange, totalPaid, totalBank);


                var invoiceDetails = (from sd in salesdetailes
                                      join s in sales
                                          on sd.InvoiceNo equals s.Invoiceno
                                      join t in thirdparties
                                          on s.ThirdPartyID equals t.ID into thirdJoin
                                      from t in thirdJoin.DefaultIfEmpty() // Left join
                                      where s.Billtype == "صدرت"
                                      let saleDateTime = DateTime.TryParseExact(
                                                             (s.TDate ?? "").Trim() + " " + (s.TTime ?? "").Trim(),
                                                             "yyyy-MM-dd HH:mm:ss",
                                                             CultureInfo.InvariantCulture,
                                                             DateTimeStyles.None,
                                                             out DateTime parsedDt) ? parsedDt : (DateTime?)null
                                      where saleDateTime.HasValue
                                            && saleDateTime.Value >= fromDateTime
                                            && saleDateTime.Value <= toDateTime
                                      select new
                                      {
                                          s.Discount,
                                          productnumber = sd.ProductNo,
                                          description = sd.TDDesc,
                                          price = sd.ItemPrice,
                                          quantity = sd.Quantity,
                                          total = sd.Total,
                                          totaldet = s.TotalAmount,
                                          customername = t != null ? t.Name : "",
                                          invoiceid = sd.InvoiceNo,
                                          ReceivedAmount = payments
                                                               .Where(p => p.InvoiceNo == s.Invoiceno)
                                                               .Sum(p => (decimal?)p.Paid ?? 0m)
                                      }).ToList();
                // فلترة الفواتير اللي لم تُستلم بالكامل
                var filteredInvoices = invoiceDetails
                    .Where(x => x.ReceivedAmount < Convert.ToDecimal(x.totaldet))
                    .ToList();

                // التأكد من وجود الجدول داخل الـ DataSet
                if (!Ds.Tables.Contains("invoicedetailes"))
                {
                    var dt3 = new DataTable("invoicedetailes");
                    dt3.Columns.Add("Discount", typeof(decimal));
                    dt3.Columns.Add("productnumber", typeof(string));
                    dt3.Columns.Add("description", typeof(string));
                    dt3.Columns.Add("price", typeof(decimal));
                    dt3.Columns.Add("quantity", typeof(decimal));
                    dt3.Columns.Add("total", typeof(decimal));
                    dt3.Columns.Add("invoiceid", typeof(string));
                    dt3.Columns.Add("totaldet", typeof(decimal));
                    dt3.Columns.Add("customername", typeof(string));
                    dt3.Columns.Add("ReceivedAmount", typeof(decimal));
                    Ds.Tables.Add(dt3);
                }

                // إضافة البيانات للـ DataTable
                var dtTable = Ds.Tables["invoicedetailes"];
                dtTable.Clear();

                foreach (var item in filteredInvoices)
                {
                    dtTable.Rows.Add(
                        item.Discount,
                        item.productnumber,
                        item.description,
                        item.price,
                        item.quantity,
                        item.total,
                        item.invoiceid,
                        item.totaldet,
                        item.customername,
                        item.ReceivedAmount
                    );
                }

            }
            else if (RBItemsales.Checked)
            {
                RD = new Summaryitems();

                if (!Ds.Tables.Contains("productitems"))
                    Ds.Tables.Add(new DataTable("productitems"));

                var sales = _IUW.sales.GetAll().ToList();
                var salesdetailes = _IUW.salesdetailes.GetAll().ToList();
                var productitems = _IUW.productitems.GetAll().ToList();
                var items = _IUW.items.GetAll().ToList();
                var unittypes = _IUW.unittypes.GetAll().ToList();

                var query = from s in sales
                            let saleDateTime = DateTime.TryParse($"{s.TDate:yyyy-MM-dd} {s.TTime:HH:mm:ss}", out var saleDateTimeResult) ? saleDateTimeResult : DateTime.MinValue
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
                                Quantity = g.Sum(x => decimal.TryParse(x.pi?.Quantity, out var q) ? q : 0),
                                Total = g.Sum(x => (decimal.TryParse(x.pi?.Quantity, out var q) ? q : 0) * (g.Key.ItemPrice ?? 0)),
                                QBD = g.Sum(x => (decimal.TryParse(x.pi?.Quantity, out var q) ? q : 0) -  + (g.Key.ItemQty ?? 0))
                            };

                var result = query.OrderBy(x => x.ID).ToList();
                var dt = Ds.Tables["productitems"];
                foreach (var item in result)
                {
                    var row = dt.NewRow();
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

            // إعداد التقرير
            RD.SetDataSource(Ds);
            RD.SetParameterValue("SalesDate", $"من {DTF.Value:dd-MM-yyyy} إلي {DTT.Value:dd-MM-yyyy}");
            RD.SetParameterValue("English_Shop_name", DC.ENName);
            RD.SetParameterValue("CompanyName", DC.Name);
            FR.CRV.ReportSource = RD;
            FR.Show();
        }

    }
}

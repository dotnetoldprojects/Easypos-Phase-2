using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using UOW;

namespace GUIForms.helpers
{
    public static class SalesHelper
    {
        public static void SaveSaleWithDetails(
            sale sale,
            List<salesdetaile> details,
            IUnitofwork _IUW)
        {
            // حفظ الفاتورة الأساسية
            _IUW.sales.Insert(sale);
            _IUW.Complete();
            // حفظ التفاصيل
            foreach (var item in details)
            {
                item.InvoiceNo = sale.Invoiceno;
                _IUW.salesdetailes.Insert(item);
                //_IUW.invtransactions.Insert(new invtransaction
                //{
                //    Proid = int.Parse(item.ProductNo.ToString()),
                //    Quantity = int.Parse(item.Quantity.ToString()),
                //    Date = DateTime.Now.ToString("dd-MM-yyyy"),
                //    Credit = 0,
                //    Dipt = decimal.Parse(item.ItemPrice.ToString()),
                //    type = "Sales",
                //    transid = sale.Invoiceno
                //});
                _IUW.Complete();
            }
            _IUW.Complete();
        }

        public static void EditSaleWithDetails(
            sale sale,
            List<salesdetaile> details,
            IUnitofwork _IUW)
                {
                    // حفظ الفاتورة الأساسية
                    _IUW.sales.Update(sale);
                    _IUW.Complete();
                    // حفظ التفاصيل
                    foreach (var item in details)
                    {
                        item.InvoiceNo = sale.Invoiceno;
                        _IUW.salesdetailes.Insert(item);
                    }

                    _IUW.Complete();
                }

        public static void SavepriceWithDetails(
    price price,
    List<pricedetaile> details,
    IUnitofwork _IUW)
        {
            // حفظ الفاتورة الأساسية
            _IUW.prices.Insert(price);
            _IUW.Complete();
            // حفظ التفاصيل
            foreach (var item in details)
            {
                item.Priceid = price.ID;
                _IUW.pricedetailes.Insert(item);
            }
            _IUW.Complete();
        }

        public static void EditPriceWithDetails(
    price price,
    List<pricedetaile> details,
    IUnitofwork _IUW)
        {
            // حفظ الفاتورة الأساسية
            _IUW.prices.Update(price);
            _IUW.Complete();
            // حفظ التفاصيل
            foreach (var item in details)
            {
                _IUW.pricedetailes.Delbyid(Convert.ToInt32(item.ID));
                item.Priceid = price.ID;
                _IUW.pricedetailes.Update(item);
            }
            _IUW.Complete();
        }



        public static void Savetransactions(int inv, int? TP, decimal? paid, string type, IUnitofwork _IUW, string date)
        {
            var Gp = _IUW.payments.GetAll().LastOrDefault();
            transaction trn = new transaction();
            trn.Invoiceno = inv;
            trn.Paynum = Gp.paymentNo;
            trn.TDate = date;
            trn.Type = type;
            trn.ThirdPartyID = TP;
            trn.Paid = paid;
            if (Gp.Bank > 0 && Gp.Cash > 0)
            {
                trn.Paytype = "Cash & Bank";
            }
            else if (Gp.Cash > 0)
            {
                trn.Paytype = "نقدي";
            }
            else if (Gp.Bank > 0) {
                trn.Paytype = "بنكي";
            }
            trn.Note = null;
            _IUW.transactions.Insert(trn);
            _IUW.Complete();
        }
    }
}

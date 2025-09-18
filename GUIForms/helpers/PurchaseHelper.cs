using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UOW;

namespace GUIForms.helpers
{
    public static class PurchaseHelper
    {
        public static void SavePurchaseWithDetails(
            purchase pur,
            List<purchasedetaile> details,
            IUnitofwork _IUW)
        {
            // حفظ الفاتورة الأساسية
            _IUW.purchases.Insert(pur);
            _IUW.Complete();
            // حفظ التفاصيل
            foreach (var item in details)
            {
                item.InvoiceNo = pur.Invoiceno;
                _IUW.purchasedetailes.Insert(item);


                //_IUW.invtransactions.Insert(new invtransaction
                //{
                //    Proid = int.Parse(item.ProductNo.ToString()),
                //    Quantity = int.Parse(item.Quantity.ToString()),
                //    Date = DateTime.Now.ToString("dd-MM-yyyy"),
                //    Credit = 0,
                //    Dipt = decimal.Parse(item.ItemPrice.ToString()),
                //    type = "Purchase",
                //    transid = item.InvoiceNo
                //});
                _IUW.Complete();
            }

            _IUW.Complete();
        }

        public static void EditPurchaseWithDetails(
            purchase pur,
            List<purchasedetaile> details,
    IUnitofwork _IUW)
        {
            // حفظ الفاتورة الأساسية
            _IUW.purchases.Update(pur);
            _IUW.Complete();
            // حفظ التفاصيل
            foreach (var item in details)
            {
                item.InvoiceNo = pur.Invoiceno;
                _IUW.purchasedetailes.Insert(item);
            }

            _IUW.Complete();
        }
    }
}

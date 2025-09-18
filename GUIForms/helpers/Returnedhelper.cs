using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOW;

namespace GUIForms.helpers
{
    public static class Returnedhelper
    {
        public static void SaveReturneWithDetails(
            returned ret,
            List<returnedetaile> details,
            IUnitofwork _IUW,string type)
        {
            // حفظ الفاتورة الأساسية
            _IUW.returneds.Insert(ret);
            _IUW.Complete();
            // حفظ التفاصيل
            foreach (var item in details)
            {
                item.InvoiceNo = ret.Invoiceno;
                _IUW.returnedetailes.Insert(item);


                //_IUW.invtransactions.Insert(new invtransaction
                //{
                //    Proid = int.Parse(item.ProductNo.ToString()),
                //    Quantity = int.Parse(item.Quantity.ToString()),
                //    Date = DateTime.Now.ToString("dd-MM-yyyy"),
                //    Credit = type == "Returned Sales" ? decimal.Parse(ret.TotalAmount) : 0,
                //    Dipt = type == "Returned Purchases" ? decimal.Parse(ret.TotalAmount) : 0,
                //    type = type,
                //    transid = item.InvoiceNo
                //});
                _IUW.Complete();
            }

            _IUW.Complete();
        }
    }
}

using Domain.Models;
using GUIForms.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using UOW;

namespace GUIForms.helpers
{
    public class Getallsales
    {
        IUnitofwork _IUW;
        public Getallsales()
        {
            _IUW = new Unitofwork(new EasyposEntities());
        }
        public List<SaleViewModel> GetSaleslist()
        {
            var result = _IUW.sales.GetAll().GroupJoin(_IUW.payments.GetAll(),
                        sale => sale.Invoiceno,
                        payment => payment.InvoiceNo,
                        (sale, payments) => new { sale, payments = payments.DefaultIfEmpty() })
                        .SelectMany(
                            x => x.payments,
                            (x, payment) => new { x.sale, payment })
                        .GroupJoin(_IUW.thirdparties.GetAll(),
                            x => x.sale.ThirdPartyID,
                            tp => tp.ID,
                            (x, thirdParties) => new { x.sale, x.payment, thirdParties = thirdParties.DefaultIfEmpty() })
                        .SelectMany(
                            x => x.thirdParties,
                            (x, thirdParty) => new { x.sale, x.payment, thirdParty })
                        .GroupJoin(_IUW.UBLS.GetAll(),
                            x => x.sale.Invoiceno,
                            ubl => ubl.Invoicenumber,
                            (x, ubls) => new { x.sale, x.payment, x.thirdParty, ubls = ubls.DefaultIfEmpty() })
                        .SelectMany(
                            x => x.ubls,
                            (x, ubl) => new SaleViewModel
                            {
                                Invoiceno = x.sale.Invoiceno,
                                TDate = x.sale.TDate,
                                TTime = x.sale.TTime,
                                NonVatTotal = x.sale.NonVatTotal,
                                Discount = x.sale.Discount,
                                VatAmount = x.sale.VatAmount,
                                TotalAmount = x.sale.TotalAmount,
                                Type = x.sale.Billtype.ToString(),
                                Cash = x.payment?.Cash ?? 0,
                                Bank = x.payment?.Bank ?? 0,
                                ThirdPartyID = x.thirdParty?.ID,
                                ThirdPartyName = x.thirdParty?.Name ?? "عميل افتراضي",
                                Phone = x.thirdParty.MobileNumber,
                                Status = ubl?.Status ?? "لم تسجل",
                                Note = x.sale.Note
                            }).OrderByDescending(x => x.Invoiceno).ToList();

                        return result;
        }
        public List<PurchaseViewModel> GetPurlist()
        {
            var result = _IUW.purchases.GetAll()
    .GroupJoin(_IUW.paymentouts.GetAll(),
        pur => pur.Invoiceno,
        payout => payout.InvoiceNo,
        (pur, payout) => new { pur, payout = payout.DefaultIfEmpty() })
    .SelectMany(
        x => x.payout,
        (x, payout) => new { x.pur, payout })
    .GroupJoin(_IUW.thirdparties.GetAll(),
        x => x.pur.ThirdPartyID,
        tp => tp.ID,
        (x, thirdParties) => new { x.pur, x.payout, thirdParties = thirdParties.DefaultIfEmpty() })
    .SelectMany(
        x => x.thirdParties,
        (x, thirdParty) => new PurchaseViewModel
        {
            Invoiceno = x.pur.Invoiceno,
            TDate = x.pur.TDate,
            TTime = x.pur.TTime,
            NonVatTotal = x.pur.NonVatTotal,
            Discount = x.pur.Discount,
            VatAmount = x.pur.VatAmount,
            TotalAmount = x.pur.TotalAmount,
            Type = x.pur.Billtype.ToString(),
            Cash = x.payout?.Cash ?? 0,
            Bank = x.payout?.Bank ?? 0,
            ThirdPartyID = thirdParty?.ID,
            ThirdPartyName = thirdParty?.Name ?? "مورد افتراضي",
        }).OrderByDescending(x => x.Invoiceno).ToList();
            return result;
        }
        public List<PricesViewModel> GetPricelist()
        {
            var result = (from price in _IUW.prices.GetAll()
                          join thirdParty in _IUW.thirdparties.GetAll()
                          on price.ThirdPartyID equals thirdParty.ID into gj
                          from subThirdParty in gj.DefaultIfEmpty() // Left join
                          orderby price.ID descending
                          select new PricesViewModel
                          {
                              ID = price.ID,
                              NonVatTotal = price.NonVatTotal,
                              Discount = price.Discount,
                              VatAmount = price.VatAmount,
                              TotalAmount = price.TotalAmount,
                              Datefrom = price.Datefrom,
                              Dateto = price.Dateto,
                              ThirdPartyName = subThirdParty != null ? subThirdParty.Name : null
                          }).ToList();

            return result;
        }
    }
}

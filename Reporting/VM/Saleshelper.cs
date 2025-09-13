using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using UOW;

namespace Reporting.VM
{
    public class Saleshelper
    {
        IUnitofwork _IUOW;
        public Saleshelper()
        {
            _IUOW = new Unitofwork(new EasyposEntities());
        }

        private class VatSummary
        {
            public double Total { get; set; }
            public double Vat { get; set; }
        }

        private bool IsValidDateRange(string DTF, string DTT, out DateTime startDate, out DateTime endDate)
        {
            endDate = default;
            return DateTime.TryParse(DTF, out startDate) && DateTime.TryParse(DTT, out endDate);
        }

        private VatSummary Getsales(string DTF, string DTT)
        {
            if (!IsValidDateRange(DTF, DTT, out var startDate, out var endDate))
                return new VatSummary();

            var salesQuery = _IUOW.sales.GetAll()
                .Where(s => s.Billtype == "صدرت"
                            && DateTime.TryParse(s.TDate, out var date)
                            && date >= startDate && date <= endDate);

            return salesQuery
                .GroupBy(s => 1)
                .Select(g => new VatSummary
                {
                    Total = g.Sum(x => x.NonVatTotal) - g.Sum(x => x.Discount),
                    Vat = g.Sum(x => x.VatAmount)
                })
                .FirstOrDefault() ?? new VatSummary();
        }

        private VatSummary Getpurchasestring(string DTF, string DTT)
        {
            if (!IsValidDateRange(DTF, DTT, out var startDate, out var endDate))
                return new VatSummary();

            var purchaseQuery = _IUOW.purchases.GetAll()
                .Where(p => p.Billtype == "صدرت"
                            && DateTime.TryParse(p.TDate, out var date)
                            && date >= startDate && date <= endDate
                            && p.VatAmount != null && p.VatAmount != 0);

            return purchaseQuery
                .GroupBy(p => 1)
                .Select(g => new VatSummary
                {
                    Total = g.Sum(x => x.NonVatTotal),
                    Vat = g.Sum(x => x.VatAmount)
                })
                .FirstOrDefault() ?? new VatSummary();
        }

        private VatSummary Getexpences(string DTF, string DTT)
        {
            var expensesQuery = _IUOW.expenses.GetAll();

            if (IsValidDateRange(DTF, DTT, out var startDate, out var endDate))
            {
                expensesQuery = expensesQuery.Where(s => DateTime.TryParse(s.CDate, out var date)
                                                        && date >= startDate && date <= endDate);
            }

            return expensesQuery
                .GroupBy(s => 1)
                .Select(g => new VatSummary
                {
                    Total = g.Sum(x => double.TryParse(x.Amount.ToString(), out var a) ? a : 0),
                    Vat = 0
                })
                .FirstOrDefault() ?? new VatSummary();
        }

        public List<Salesonetax> Getonetax(string DTF, string DTT)
        {
            var salesResult = Getsales(DTF, DTT);
            var purchaseResult = Getpurchasestring(DTF, DTT);
            var expensesResult = Getexpences(DTF, DTT);

            return new List<Salesonetax>
        {
            new Salesonetax
            {
                Total = salesResult.Total - purchaseResult.Total - expensesResult.Total,
                Vat = salesResult.Vat - purchaseResult.Vat - expensesResult.Vat
            }
        };
        }

        public List<Saleslist> Getsalesdetails(string DTF, string DTT)
        {
            var query = _IUOW.sales.GetAll()
                .Where(s => s.Billtype == "صدرت")
                .Join(_IUOW.salesdetailes.GetAll(), s => s.Invoiceno, d => d.InvoiceNo, (s, d) => new { s, d })
                .Where(x => !string.IsNullOrEmpty(x.d.ProductNo.ToString()));

            if (IsValidDateRange(DTF, DTT, out var startDate, out var endDate))
            {
                query = query.Where(s => DateTime.TryParse(s.s.TDate, out var date)
                                         && date >= startDate && date <= endDate);
            }

            return query.Select(x => new Saleslist
            {
                Invoiceno = x.s.Invoiceno,
                TDate = x.s.TDate,
                TTime = x.s.TTime,
                ProductNo = Convert.ToInt32(x.d.ProductNo.ToString()),
                TDDesc = x.d.TDDesc,
                Quantity = x.d.Quantity,
                ItemPrice = x.d.ItemPrice,
                Total = Convert.ToDouble(x.d.Total),
                TotalAmount = Convert.ToDouble(x.s.TotalAmount)
            }).ToList();
        }

        public List<Saleslist> Getsaleslist(string DTF, string DTT)
        {
            var allSales = _IUOW.sales.GetAll().Where(s => s.Billtype == "صدرت");

            if (IsValidDateRange(DTF, DTT, out var startDate, out var endDate))
            {
                allSales = allSales.Where(s => DateTime.TryParse(s.TDate, out var date)
                                               && date >= startDate && date <= endDate);
            }

            var joinedSales = allSales
                .Join(_IUOW.thirdparties.GetAll(), s => s.ThirdPartyID, tp => tp.ID, (s, tp) => new { s, tp })
                .GroupJoin(_IUOW.payments.GetAll(), st => st.s.Invoiceno, p => p.InvoiceNo,
                    (st, payments) => new
                    {
                        st.s.Invoiceno,
                        st.tp.Name,
                        st.s.TDate,
                        PaymentMethod = payments.Select(x => x.PaymentMethod).FirstOrDefault(),
                        st.s.NonVatTotal,
                        st.s.Discount,
                        st.s.VatAmount,
                        st.s.TotalAmount,
                        Paid = payments.Sum(p => p.Paid),
                        Remaining = decimal.Parse(st.s.TotalAmount) - payments.Sum(p => p.Paid)
                    });

            return joinedSales.Select(item => new Saleslist
            {
                Invoiceno = item.Invoiceno,
                Name = item.Name,
                TDate = item.TDate,
                PaymentMethod = item.PaymentMethod,
                NonVatTotal = item.NonVatTotal,
                Discount = item.Discount,
                VatAmount = item.VatAmount,
                TotalAmount = double.Parse(item.TotalAmount.ToString()),
                Paid = double.Parse(item.Paid.ToString()),
                Remaining = double.Parse(item.Remaining.ToString())
            }).ToList();
        }
        public List<UBLDtos> Getsaleszatcalist(string DTF, string DTT)
        {
            var allSales = _IUOW.UBLS.GetAll()
                .Where(ubl => ubl.Status == "سجلت")
                .GroupJoin(
                    _IUOW.sales.GetAll(),
                    ubl => ubl.Saleid,
                    sale => sale.Invoiceno,
                    (ubl, saleGroup) => new { ubl, saleGroup }
                )
                .SelectMany(
                    x => x.saleGroup.DefaultIfEmpty(),
                    (x, sale) => new
                    {
                        x.ubl.Saleid,
                        x.ubl.Uuid,
                        NonVatTotal = sale?.NonVatTotal ?? 0,
                        Discount = sale?.Discount ?? 0,
                        VatAmount = sale?.VatAmount ?? 0,
                        TotalAmount = sale?.TotalAmount,
                        TDate = sale?.TDate,
                    }
                )
                .ToList();

            // فلترة بالتاريخ
            if (IsValidDateRange(DTF, DTT, out var startDate, out var endDate))
            {
                allSales = allSales
                    .Where(s => DateTime.TryParse(s.TDate, out var date)
                                && date >= startDate && date <= endDate)
                    .ToList();
            }

            // تحويل للـ Saleslist
            return allSales.Select(item => new UBLDtos
            {
                Invoiceno = (int)item.Saleid,
                Name = item.Uuid,
                NonVatTotal = item.NonVatTotal,
                Discount = item.Discount,
                VatAmount = item.VatAmount,
                TotalAmount = double.Parse(item.TotalAmount),
                TDate = item.TDate,

            }).ToList();
        }

        public List<EWT> GetExpenselist(string DTF, string DTT, int Tid)
        {
            var allExpenses = _IUOW.expenses.GetAll();

            if (IsValidDateRange(DTF, DTT, out var startDate, out var endDate))
            {
                allExpenses = allExpenses.Where(e => DateTime.TryParse(e.CDate, out var d)
                                                    && d >= startDate && d <= endDate);
            }

            if (Tid > 0)
            {
                allExpenses = allExpenses.Where(e => e.Typeid == Tid);
            }

            var allTypes = _IUOW.expencestypes.GetAll().ToList();

            var filtered = from exp in allExpenses.ToList()
                           join type in allTypes on exp.Typeid equals type.Id into joined
                           from jt in joined.DefaultIfEmpty()
                           select new EWT
                           {
                               ID = exp.ID,
                               Description = exp.Description,
                               Amount = exp.Amount,
                               CDate = exp.CDate,
                               Vat = exp.Vat,
                               Expencestypename = jt?.Expencestypename
                           };

            return filtered.ToList();
        }
    }
}

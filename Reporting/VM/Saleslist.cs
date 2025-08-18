using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.VM
{
    public class Saleslist
    {
        public int Invoiceno { get; set; }
        public string TDate { get; set; }
        public string TTime { get; set; }
        public double NonVatTotal { get; set; }
        public double Discount { get; set; }
        public double VatAmount { get; set; }
        public double TotalAmount { get; set; }
        public string Billtype { get; set; }
        public string Name { get; set; }
        public string Taxnumber { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public int ProductNo { get; set; }
        public string TDDesc { get; set; }
        public string UnitType { get; set; }
        public int Unitid { get; set; }
        public double ItemPrice { get; set; }
        public double Quantity { get; set; }
        public double Subtotal { get; set; }
        public double Discountdet { get; set; }
        public double Totafterdiscount { get; set; }
        public double Total { get; set; }
        public double Paid { get; set; }
        public string PaymentMethod { get; set; }
        public string Purchaseorder { get; set; }
        public string Refrancenumber { get; set; }
        public string Projectname { get; set; }
        public string Type { get; set; }
        public string Supplierbill { get; set; }
        public double Remaining { get; set; }
        public string Note { get; set; }
        public string Tailordate { get; set; }
        public string TailorReceiptdate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIForms.models
{
    public class SaleViewModel
    {
        public int? paymentNo { get; set; }
        public int Invoiceno { get; set; }
        public string TDate { get; set; }
        public string TTime { get; set; }
        public double NonVatTotal { get; set; }
        public double Discount { get; set; }
        public double VatAmount { get; set; }
        public string TotalAmount { get; set; }
        public decimal Cash { get; set; }
        public decimal Bank { get; set; }
        public int? ThirdPartyID { get; set; }
        public string ThirdPartyName { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Invoicenumber { get; set; }
        public string Note { get; set; }

        public DateTime TDateValue => DateTime.Parse(TDate);
    }
}

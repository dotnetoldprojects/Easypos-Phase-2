using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIForms.models
{
    public class PricesViewModel
    {
        public int ID { get; set; }
        public double? NonVatTotal { get; set; }
        public double? Discount { get; set; }
        public double? VatAmount { get; set; }
        public double? TotalAmount { get; set; }
        public string ThirdPartyName { get; set; }
        public string Datefrom { get; set; }
        public string Dateto { get; set; }
    }
}

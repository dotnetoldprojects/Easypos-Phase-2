using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.VM
{
    public class UBLDtos
    {
        public int Invoiceno { get; set; }
        public string Name { get; set; }
        public double NonVatTotal { get; set; }
        public double Discount { get; set; }
        public double VatAmount { get; set; }
        public double TotalAmount { get; set; }
        public string TDate { get; set; }
    }
}

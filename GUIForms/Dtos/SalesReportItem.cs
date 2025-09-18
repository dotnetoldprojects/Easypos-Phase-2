using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIForms.Dtos
{
    public class SalesReportItem
    {
        public int ID { get; set; }
        public string Itemname { get; set; }
        public string Date { get; set; }
        public double Itemqty { get; set; }
        public int Quantity { get; set; }
        public double Remining { get; set; }
        public string Invoiceno { get; set; }
        public string Type { get; set; }
    }

}

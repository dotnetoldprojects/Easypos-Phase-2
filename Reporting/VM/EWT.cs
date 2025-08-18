using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.VM
{
    public class EWT
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public Nullable<double> Amount { get; set; }
        public string CDate { get; set; }
        public Nullable<double> Vat { get; set; }
        public string Expencestypename { get; set; }
    }
}

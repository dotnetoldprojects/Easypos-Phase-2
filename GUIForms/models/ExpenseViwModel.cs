using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIForms.models
{
    public class ExpenseViwModel
    {
        public int ID { get; set; }
        public double? Amount { get; set; }
        public string CDate { get; set; }
        public double? Vat { get; set; }
        public string Description { get; set; }
        public string TypeName { get; set; }
    }
}

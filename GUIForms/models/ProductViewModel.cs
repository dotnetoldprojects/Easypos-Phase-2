using org.apache.xerces.impl.xs.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIForms.models
{
    public class ProductViewModel
    {
        public int ProductNo { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryNo { get; set; }
        public int Unitid { get; set; }
        public int StocksOnHand { get; set; }
        public bool ShowInPOS { get; set; }
        public bool AllowInventory { get; set; }
        public int ReorderLevel { get; set; }
        public int? Order { get; set; } // Added missing property to fix CS0117  
    }
}

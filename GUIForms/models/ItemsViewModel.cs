using org.apache.xerces.impl.xs.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIForms.models
{
    public class ItemsViewModel
    {
        public int ID { get; set; }
        public string Itemname { get; set; }
        public double Itemprice { get; set; }
        public double Itemqty { get; set; }
        public int OpeningBalance { get; set; }
        public int Unitid { get; set; }
        public string UnitName { get; set; }
    }
}

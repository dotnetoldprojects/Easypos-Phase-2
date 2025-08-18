using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public decimal Quantity { get; set; }
        public string UnitCode { get; set; } = "PCE";
        public decimal UnitPrice { get; set; }
        public decimal VatRate { get; set; } = 15;
    }
}

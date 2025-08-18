using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class InvoiceLine
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal NetAmount => Product.UnitPrice * Quantity;
        public decimal VatAmount => NetAmount * Product.VatRate;
        public decimal TotalAmount => NetAmount + VatAmount;
    }
}

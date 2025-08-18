using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public Company Issuer { get; set; }
        public Customer Buyer { get; set; }
        public Device Device { get; set; }
        public DateTime IssueDate { get; set; }
        public List<InvoiceLine> Lines { get; set; }
        public decimal TotalNet => Lines.Sum(l => l.NetAmount);
        public decimal TotalVat => Lines.Sum(l => l.VatAmount);
        public decimal TotalAmount => TotalNet + TotalVat;
    }
}

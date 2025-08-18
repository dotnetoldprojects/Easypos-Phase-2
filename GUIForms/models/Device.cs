using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Device
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string SerialNumber { get; set; }
        public string CsrToken { get; set; }
        public string SecretToken { get; set; }
        public Guid CompanyId { get; set; } // أنسب من تضمين كائن كامل

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string CRN { get; set; }
        public string Address { get; set; }
        public string CommonName { get; set; }
        public string SerialNumber { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string OrganizationUnitName { get; set; }
        public string CountryName { get; set; }
        public string InvoiceType { get; set; }
        public string LocationAddress { get; set; }
        public string IndustryBusinessCategory { get; set; }
        public List<Device> Devices { get; set; }
    }
}

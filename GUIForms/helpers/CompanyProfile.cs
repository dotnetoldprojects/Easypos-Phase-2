using Domain.Models;
using org.apache.xerces.xni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GUI.Helpers
{
    public class CompanyProfile 
    {
        public Company _company = new Company
        {
            Id = Guid.Empty,
            Name = string.Empty,
            TaxNumber = string.Empty,
            CRN = string.Empty,
            CommonName = $"TST-{string.Empty}-{string.Empty}",
            SerialNumber = $"1-{string.Empty}|2-Easypos|3-{Guid.NewGuid()}",
            OrganizationIdentifier = string.Empty,
            OrganizationUnitName = string.Empty,
            CountryName = "SA",
            InvoiceType = "1100",
            Address = "Jeddah",
            LocationAddress = "Jeddah, Saudi Arabia",
            IndustryBusinessCategory = "Foodes"
        };
        public Company GetCompanyProfile(Guid FixedId,string CompanyName, string Tax, string CRN) 
        { 
            _company.Id = FixedId;
            _company.Name = CompanyName;
            _company.TaxNumber = Tax;
            _company.CRN = CRN;
            _company.CommonName = $"TST-{CompanyName}-{Tax}";
            _company.SerialNumber = $"1-{CompanyName}|2-Easypos|3-{Guid.NewGuid()}";
            _company.OrganizationIdentifier = Tax;
            _company.OrganizationUnitName = CompanyName;
            return _company; 
        } 
        public void AddDevice(Device device) 
        { 
            if (_company.Devices == null) 
                _company.Devices = new List<Device>(); 
            _company.Devices.Add(device); 
        } 
        public void AddDevices(IEnumerable<Device> devices) 
        { 
            if (_company.Devices == null) 
                
                _company.Devices = new List<Device>(); 
            _company.Devices.AddRange(devices); 
        }
    }
}

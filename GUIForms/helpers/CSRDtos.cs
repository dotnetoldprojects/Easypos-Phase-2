using java.lang;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zatca.EInvoice.SDK;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace GUI.Helpers
{
    public class CSRDtos
    {
        public CsrGenerationDto _CSRDTO;
        public void GenerateCSR(string commonName, 
                                string serialNumber, 
                                string organizationIdentifier, 
                                string organizationUnitName, 
                                string organizationName, 
                                string countryName, 
                                string invoiceType, 
                                string locationAddress, 
                                string industryBusinessCategory)
        {
            var generator = new CsrGenerator();
            _CSRDTO = new CsrGenerationDto(
                commonName: commonName,
                serialNumber: serialNumber,
                organizationIdentifier: organizationIdentifier,
                organizationUnitName: organizationUnitName,
                organizationName: organizationName,
                countryName: countryName,
                invoiceType: invoiceType,
                locationAddress: locationAddress,
                industryBusinessCategory: industryBusinessCategory
            );
            var result = generator.GenerateCsr(_CSRDTO, EnvironmentType.NonProduction, pemFormat: true);
            if (!result.IsValid)
            {
                MessageBox.Show("❌ Fail Generate Data Company Certificate");
                foreach (var error in result.ErrorMessages)
                    MessageBox.Show("- " + error);
            }
            else
            {
                string csrBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(result.Csr));
                string keyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(result.PrivateKey));
                File.WriteAllText("Data/Certificates/certificate.csr", csrBase64);
                File.WriteAllText("Data/Certificates/private.key", keyBase64);
                MessageBox.Show("✅ Done Generate Data Company Certificate And PrivateKey ... ");
            }
        }
    }
}

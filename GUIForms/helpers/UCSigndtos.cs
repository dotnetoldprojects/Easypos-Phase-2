using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Helpers
{
    public class UCSigndtos
    {
        public string CSRToken { get; set; }
        public string SercretToken { get; set; }
        public async Task Getcertificate()
        {
            CSR csrt = new CSR();
            var data = File.ReadAllText("Data/Certificates/CSR.Json");
            var csrtdata = JsonConvert.DeserializeObject<CSR>(data);
            var payload = new { compliance_request_id = csrtdata.requestID };
            var json = JsonConvert.SerializeObject(payload);
            string credentials = $"{csrtdata.binarySecurityToken}:{csrtdata.secret}";
            string base64Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Accept-Version", "V2");
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Token);
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await http.PostAsync(
                "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/production/csids",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var body = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
            {
                MessageBox.Show("❌ Fail Generated Certification \n" + body);
                return;
            }
            var csrtbody = JsonConvert.DeserializeObject<CSR>(body);
            CSRToken = csrtbody.binarySecurityToken;
            SercretToken = csrtbody.secret;
            File.WriteAllText("Data/Certificates/CSR.Json", body);
            MessageBox.Show("✅ Done Save Token ");
        }
    }
}

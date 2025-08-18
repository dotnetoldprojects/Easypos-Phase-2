using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Helpers
{
    public class UCDtos
    {
        public string CSRToken { get; set; }
        public string SercretToken { get; set; }
        public async Task UploadCsrAsync()
        {
            var data = File.ReadAllText("Data/Certificates/certificate.csr");
            var payload = new { csr = data };
            var json = JsonConvert.SerializeObject(payload);

            var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Accept-Version", "V2");
            http.DefaultRequestHeaders.Add("OTP", "123345");
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await http.PostAsync(
                "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var body = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
            {
                MessageBox.Show("❌ Fail Upload Data Company" + body);
                return;
            }

            MessageBox.Show("✅ Done Upload Data Company");
            //Console.WriteLine(body +"\n");
            var obj = JsonConvert.DeserializeObject(body);
            var csrt = JsonConvert.DeserializeObject<CSR>(body);
            CSRToken = csrt.binarySecurityToken;
            SercretToken = csrt.secret;
            File.WriteAllText("Data/Certificates/CSR.Json", body);
            MessageBox.Show("Request Token Hase Saved Success .");
        }
    }
}

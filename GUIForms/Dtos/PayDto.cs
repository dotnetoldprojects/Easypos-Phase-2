using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIForms.Dtos
{
    public class PayDto
    {
        public int PaymentNo { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Remaining { get; set; }
        public string ThirdPartyName { get; set; }
    }
}

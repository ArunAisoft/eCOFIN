using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCOFIN.Application.DTOs.Masters
{
    public class CurrencyDto
    {
        public string CurrencyCode { get; set; } = string.Empty;
        public string? CurrencyName { get; set; }
        public string? Country { get; set; }
        public string? Symbol { get; set; }
        public string? Location { get; set; }
        public string? Username { get; set; }
        public string? ObjectStatus { get; set; }
    }
}
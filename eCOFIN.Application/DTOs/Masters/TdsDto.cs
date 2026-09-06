using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCOFIN.Application.DTOs.Masters
{
    public class TdsReportDto
    {
        public string Tdscode { get; set; }
        public string? Tdsdescription { get; set; }
        public decimal? Tdsperc { get; set; }
        public string? ObjectStatus { get; set; }
        public string? TdsAccount { get; set; }
    }

    public class TdsCreateModel
    {
        public string Tdscode { get; set; } = string.Empty;
        public string? Tdsdescription { get; set; }
        public decimal? Tdsperc { get; set; }
        public string? ObjectStatus { get; set; }
        public string? Username { get; set; }
        public string? Location { get; set; }
    }
}
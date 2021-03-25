using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboPartnerReport
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public DateTime? ReportDate { get; set; }
        public string ReportType { get; set; }
        public string Standarts { get; set; }
        public string IsAnnex { get; set; }
        public string LetterId { get; set; }
        public DateTime? GetDate { get; set; }
        public string IsRated { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string ReportSource { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboRSource
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public string Activity { get; set; }
        public string PartnerType { get; set; }
        public string EaDref { get; set; }
        public double? EaD { get; set; }
        public string EaDformula { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public double? Maturity { get; set; }
    }
}

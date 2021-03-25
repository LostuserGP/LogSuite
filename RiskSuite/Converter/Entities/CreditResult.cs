using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CreditResult
    {
        public string Id { get; set; }
        public string ReportId { get; set; }
        public string Measure { get; set; }
        public double? Value { get; set; }
        public string IsCrisis { get; set; }
        public string TimePeriod { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
    }
}

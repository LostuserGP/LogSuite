using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CreditPartnerResult
    {
        public string Id { get; set; }
        public string ReportId { get; set; }
        public string Partner { get; set; }
        public double? Value { get; set; }
        public DateTime? AdmDate { get; set; }
        public string Measure { get; set; }
        public string AdmUser { get; set; }
    }
}

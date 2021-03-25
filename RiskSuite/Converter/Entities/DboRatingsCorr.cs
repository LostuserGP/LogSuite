using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboRatingsCorr
    {
        public string Id { get; set; }
        public string Rat1 { get; set; }
        public string Rat2 { get; set; }
        public double? Correlation { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

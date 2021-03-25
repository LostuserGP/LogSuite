using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboRatingsScale
    {
        public string Id { get; set; }
        public string Rating { get; set; }
        public double? Pd { get; set; }
        public string IsCrisis { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

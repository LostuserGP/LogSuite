using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboPfe
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public double? Pfe { get; set; }
        public string Currency { get; set; }
        public string Activity { get; set; }
        public string AccountType { get; set; }
        public string AccountDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TimePeriod { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
    }
}

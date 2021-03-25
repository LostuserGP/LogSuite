using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboReport
    {
        public string Id { get; set; }
        public string AccountType { get; set; }
        public string AccountDate { get; set; }
        public string Activity { get; set; }
        public string CountDays { get; set; }
        public string Currency { get; set; }
        public double? Probability { get; set; }
        public string Simulation { get; set; }
        public string IsCrisis { get; set; }
        public string IsProcess { get; set; }
        public string TimePeriod { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
    }
}

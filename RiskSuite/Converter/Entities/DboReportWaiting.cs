using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboReportWaiting
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public double? WaitingPeriod { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

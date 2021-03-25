using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboLettersEntity
    {
        public string Id { get; set; }
        public string LetterId { get; set; }
        public string Information { get; set; }
        public DateTime? ReportDate { get; set; }
        public string ReportType { get; set; }
        public string Status { get; set; }
    }
}

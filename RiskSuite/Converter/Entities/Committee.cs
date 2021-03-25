using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class Committee
    {
        public string Id { get; set; }
        public DateTime? CommitteeDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string InPresence { get; set; }
        public string ProtocolNumber { get; set; }
        public string CommitteeStatus { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboRatingProcess
    {
        public string Id { get; set; }
        public string Worker { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string Stage { get; set; }
        public string PartnerReport { get; set; }
        public string Partner { get; set; }
        public DateTime? StartDrat { get; set; }
        public DateTime? EndDrat { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

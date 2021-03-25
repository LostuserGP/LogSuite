using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboPartnerRise
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string PartnerTask { get; set; }
        public string IsActual { get; set; }
        public string Worker { get; set; }
        public string CoWorker { get; set; }
        public DateTime? CoFromDate { get; set; }
        public DateTime? CoToDate { get; set; }
        public string Portfolio { get; set; }
    }
}

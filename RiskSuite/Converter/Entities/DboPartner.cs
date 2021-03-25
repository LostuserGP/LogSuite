using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboPartner
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public string FullName { get; set; }
        public string ActCountry { get; set; }
        public string Sector { get; set; }
        public string Activity { get; set; }
        public string Relation { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string IsLimitByCountry { get; set; }
    }
}

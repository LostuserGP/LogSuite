using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboPartnerOwner
    {
        public string Partner { get; set; }
        public string PartnerOwner { get; set; }
        public decimal? Interest { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string CountryOwner { get; set; }
        public string Id { get; set; }
    }
}

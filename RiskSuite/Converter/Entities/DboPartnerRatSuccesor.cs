using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboPartnerRatSuccesor
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public string RatingPartner { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Note { get; set; }
        public DateTime? AdmDate { get; set; }
        public string Admuser { get; set; }
    }
}

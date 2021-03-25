using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CorporateSecurity
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public string Number { get; set; }
        public string IsAdmit { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

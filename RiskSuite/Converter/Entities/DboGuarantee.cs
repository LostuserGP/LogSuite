using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboGuarantee
    {
        public string Id { get; set; }
        public string Bank { get; set; }
        public string Company { get; set; }
        public string GarantType { get; set; }
        public double? GarantSum { get; set; }
        public string Currency { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? AdmDate { get; set; }
        public string AdmUser { get; set; }
    }
}

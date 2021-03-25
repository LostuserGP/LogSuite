using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboCurrencyRate
    {
        public string Id { get; set; }
        public string CurrencyBase { get; set; }
        public string CurrencyNeed { get; set; }
        public double? Rate { get; set; }
        public string AccountDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Activity { get; set; }
    }
}

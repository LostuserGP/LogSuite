using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboExchangeRate
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public double? Rate { get; set; }
        public DateTime? ReportDate { get; set; }
    }
}

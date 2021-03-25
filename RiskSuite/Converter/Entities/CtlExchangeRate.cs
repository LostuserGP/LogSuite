using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlExchangeRate
    {
        public int? FromCurrency { get; set; }
        public int? ToCurrency { get; set; }
        public double? Rate { get; set; }
        public string ReportDate { get; set; }
    }
}

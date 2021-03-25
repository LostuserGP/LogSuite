using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlGroupLimit
    {
        public int Id { get; set; }
        public int? GroupNumber { get; set; }
        public string GroupLimit { get; set; }
        public string BankLimit { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlFinancialStatement
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Standards { get; set; }
        public string Comments { get; set; }
        public int? CounterpartyId { get; set; }
    }
}

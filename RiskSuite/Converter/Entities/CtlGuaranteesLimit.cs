using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlGuaranteesLimit
    {
        public int Id { get; set; }
        public int? CounterpartyId { get; set; }
        public int? BankId { get; set; }
        public string AgreeFirstDate { get; set; }
        public string AgreeEndDate { get; set; }
        public string GuaranteeEndDate { get; set; }
        public string Amount { get; set; }
        public int? CurrencyId { get; set; }
        public int? GuaranteeNameId { get; set; }
    }
}

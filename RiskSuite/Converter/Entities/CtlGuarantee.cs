using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlGuarantee
    {
        public int Id { get; set; }
        public int? CounterpartyId { get; set; }
        public int? GuarantorId { get; set; }
        public int? CurrencyId { get; set; }
        public int? TypeId { get; set; }
        public string Amount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string GuaranteeNumber { get; set; }
    }
}

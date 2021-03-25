using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlInternalRatingCounterparty
    {
        public int? RatingValueId { get; set; }
        public int? RatingValueWithoutCountryId { get; set; }
        public int? RiskClassId { get; set; }
        public bool IsConservative { get; set; }
        public int? FinancialStatementId { get; set; }
        public string StartDate { get; set; }
        public int? CounterpartyId { get; set; }
        public string Analyst { get; set; }
        public string Comments { get; set; }
    }
}

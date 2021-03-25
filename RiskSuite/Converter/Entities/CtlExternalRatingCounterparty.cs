using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlExternalRatingCounterparty
    {
        public int? RatingAgencyId { get; set; }
        public int? RatingValueId { get; set; }
        public string StartDate { get; set; }
        public int? CounterpartyId { get; set; }
    }
}

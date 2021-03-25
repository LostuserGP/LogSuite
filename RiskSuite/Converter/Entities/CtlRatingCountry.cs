using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlRatingCountry
    {
        public int? RatingAgencyId { get; set; }
        public int? RatingValueId { get; set; }
        public string StartDate { get; set; }
        public int? CountryId { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CountryLatestAndLowestRating
    {
        public int? CountryId { get; set; }
        public string Country { get; set; }
        public string RatingAgency { get; set; }
        public int? RatingId { get; set; }
        public string Rating { get; set; }
    }
}

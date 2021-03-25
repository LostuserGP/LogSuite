using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CountriesRating
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string Rating { get; set; }
        public double? Pd { get; set; }
        public string RatingAgency { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string RatingType { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CountriesAgencyName
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string RatingAgency { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
    }
}

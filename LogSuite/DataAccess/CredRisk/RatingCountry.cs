using LogSuite.DataAccess.References;
using System;

namespace LogSuite.DataAccess.CredRisk
{
    public class RatingCountry
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public int RatingAgencyId { get; set; }
        public RatingAgency RatingAgency { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}

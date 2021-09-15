using System;

namespace LogSuite.Shared.Models.Operativka
{
    public class GisCountryResourceDTO
	{
        public int Id { get; set; }
        public int GisCountryId { get; set; }
        public GisCountryDTO GisCountry { get; set; }
        public DateTime Month { get; set; }
        public decimal Value { get; set; }
    }
}

using LogSuite.Shared.Helpers;
using System;

namespace LogSuite.Shared.Models.DailyReview
{
    public class GisCountryValueDTO : DayValue
	{
        public int GisCountryId { get; set; }
        public GisCountryDTO GisCountry { get; set; }
    }
}

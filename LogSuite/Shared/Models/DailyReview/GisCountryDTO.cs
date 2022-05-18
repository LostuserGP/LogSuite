using LogSuite.Shared.Models.References;
using System.Collections.Generic;

namespace LogSuite.Shared.Models.DailyReview
{
    public class GisCountryDTO
	{
        public int Id { get; set; }
        public int CountryId { get; set; }
        public CountryDTO Country { get; set; }
        public bool IsHidden { get; set; }
        public List<GisCountryResourceDTO> Resources { get; set; } = new List<GisCountryResourceDTO>();
        public int GisId { get; set; }
        public GisDTO Gis { get; set; }
        public List<GisCountryValueDTO> Values { get; set; } = new List<GisCountryValueDTO>();
    }
}

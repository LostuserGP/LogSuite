using LogSuite.Shared.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared.Models.DailyReview
{
	public class GisCountryDTO
	{
        public int Id { get; set; }
        public int CountryId { get; set; }
        public CountryDTO Country { get; set; }
        public bool IsHidden { get; set; }
        public bool IsCalculated { get; set; }
        public int Multiplicator { get; set; }
        public List<GisCountryResourceDTO> Resources { get; set; } = new List<GisCountryResourceDTO>();
        public int GisId { get; set; }
        public GisDTO Gis { get; set; }
        public List<GisCountryValueDTO> Values { get; set; } = new List<GisCountryValueDTO>();
    }
}

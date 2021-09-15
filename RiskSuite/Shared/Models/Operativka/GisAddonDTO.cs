using System.Collections.Generic;

namespace LogSuite.Shared.Models.Operativka
{
    public class GisAddonDTO
	{
        public int Id { get; set; }
        public int GisId { get; set; }
        public GisDTO Gis { get; set; }
        public string Name { get; set; }
        public string DailyReviewName { get; set; }
        public bool IsHidden { get; set; }
        public bool IsCalculated { get; set; }
        public int Multiplicator { get; set; }
        public List<GisAddonNameDTO> Names { get; set; } = new List<GisAddonNameDTO>();
        public List<GisAddonValueDTO> Values { get; set; } = new List<GisAddonValueDTO>();
    }
}

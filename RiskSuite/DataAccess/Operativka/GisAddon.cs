using System.Collections.Generic;

namespace LogSuite.DataAccess.Operativka
{
    public class GisAddon
    {
        public int Id { get; set; }
        public int GisId { get; set; }
        public Gis Gis { get; set; }
        public string Name { get; set; }
        public string DailyReviewName { get; set; }
        public bool IsHidden { get; set; }
        public bool IsCalculated { get; set; }
        public int Multiplicator { get; set; }
        public List<GisAddonName> Names { get; set; } = new List<GisAddonName>();
        public List<GisAddonValue> Values { get; set; } = new List<GisAddonValue>();
    }
}

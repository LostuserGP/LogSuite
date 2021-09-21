using System.Collections.Generic;

namespace LogSuite.DataAccess.DailyReview
{
    public class Gis
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DailyReviewName { get; set; }
        public bool IsHidden { get; set; }
        public bool IsUkraineTransport { get; set; }
        public List<GisName> Names { get; set; } = new List<GisName>();
        public List<GisAddon> Addons { get; set; } = new List<GisAddon>();
        public List<GisCountry> Countries { get; set; } = new List<GisCountry>();
        public bool NoPhg { get; set; }
        public bool NoCountry { get; set; }
        public List<GisInputName> GisInputNames { get; set; }
        public List<GisOutputName> GisOutputNames { get; set; }
        public List<GisInputValue> GisInputValues { get; set; }
        public List<GisOutputValue> GisOutputValues { get; set; }
    }
}

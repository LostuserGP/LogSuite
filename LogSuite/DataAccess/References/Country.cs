using System.Collections.Generic;
using LogSuite.DataAccess.DailyReview;

namespace LogSuite.DataAccess.References
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DailyReviewName { get; set; }
        public string NameEn { get; set; }
        public string ShortName { get; set; }
        public List<string> Names { get; set; } = new List<string>();
        public List<GisCountry> GisCountries { get; set; } = new List<GisCountry>();
    }
}

using System.Collections.Generic;

namespace LogSuite.DataAccess.References
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string ShortName { get; set; }
        public string DailyReviewName { get; set; }
        public string Ticker { get; set; }
        public IList<CountryName> Names { get; set; } = new List<CountryName>();
    }
}

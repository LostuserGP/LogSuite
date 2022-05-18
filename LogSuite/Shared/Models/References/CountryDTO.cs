using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Shared.Models.References
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string DailyReviewName { get; set; }
        public string ShortName { get; set; }
        public List<string> Names { get; set; } = new List<string>();
        public List<GisCountryDTO> GisCountries { get; set; } = new List<GisCountryDTO>();
    }
}

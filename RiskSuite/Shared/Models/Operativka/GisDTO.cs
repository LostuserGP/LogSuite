using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogSuite.Shared.Models.DailyReview
{
    public class GisDTO
	{
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter country name in russian")]
        public string Name { get; set; }
        public string DailyReviewName { get; set; }
        public bool IsHidden { get; set; }
        public bool IsUkraineTransport { get; set; }
        public List<GisNameDTO> Names { get; set; } = new List<GisNameDTO>();
        public List<GisAddonDTO> Addons { get; set; } = new List<GisAddonDTO>();
        public List<GisCountryDTO> Countries { get; set; } = new List<GisCountryDTO>();
        public bool NoPhg { get; set; }
        public bool NoCountry { get; set; }
        public List<GisInputNameDTO> GisInputNames { get; set; }
        public List<GisOutputNameDTO> GisOutputNames { get; set; }
        public List<GisInputValueDTO> GisInputValues { get; set; }
        public List<GisOutputValueDTO> GisOutputValues { get; set; }
    }
}

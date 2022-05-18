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
        public bool IsNotCalculated { get; set; }
        public bool IsTop { get; set; }
        public bool IsBottom { get; set; }
        public bool IsOneRow { get; set; }
        public bool IsNoPhg { get; set; }
        public List<string> Names { get; set; } = new List<string>();
        public List<GisAddonDTO> Addons { get; set; } = new List<GisAddonDTO>();
        public List<GisCountryDTO> Countries { get; set; } = new List<GisCountryDTO>();
        public List<string> GisInputNames { get; set; } = new List<string>();
        public List<string> GisOutputNames { get; set; } = new List<string>();
        public List<GisInputValueDTO> GisInputValues { get; set; }
        public List<GisInputValueDTO> GisOutputValues { get; set; }
    }
}

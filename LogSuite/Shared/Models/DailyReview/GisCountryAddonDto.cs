using System.Collections.Generic;

namespace LogSuite.Shared.Models.DailyReview;

public class GisCountryAddonDto
{
    public int Id { get; set; }
    public int GisCountryId { get; set; }
    public GisCountryDTO GisCountry { get; set; }
    public string Name { get; set; }
    public string DailyReviewName { get; set; }
    public bool IsHidden { get; set; }
    public bool IsCalculated { get; set; }
    public List<string> Names { get; set; } = new List<string>();
    public List<GisCountryAddonTypeDto> Types { get; set; } = new List<GisCountryAddonTypeDto>();
    public List<GisCountryAddonValueDto> Values { get; set; } = new List<GisCountryAddonValueDto>();
}
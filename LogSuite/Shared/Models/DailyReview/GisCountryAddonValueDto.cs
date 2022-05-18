namespace LogSuite.Shared.Models.DailyReview;

public class GisCountryAddonValueDto : DayValue
{
    public int GisCountryAddonId { get; set; }
    public GisCountryAddonDto GisCountryAddon { get; set; }
}
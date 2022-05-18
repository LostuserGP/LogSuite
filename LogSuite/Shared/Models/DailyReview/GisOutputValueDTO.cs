namespace LogSuite.Shared.Models.DailyReview
{
    public class GisOutputValueDTO : DayValue
    {
        public int GisId { get; set; }
        public GisDTO Gis { get; set; }
    }
}

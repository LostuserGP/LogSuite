namespace LogSuite.Shared.Models.DailyReview
{
    public class GisNameDTO
	{
		public int Id { get; set; }
		public int GisId { get; set; }
		public GisDTO Gis { get; set; }
		public string Name { get; set; }
	}
}

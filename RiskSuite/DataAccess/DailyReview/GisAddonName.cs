namespace LogSuite.DataAccess.DailyReview
{
    public class GisAddonName
    {
        public int Id { get; set; }
        public int GisAddonId { get; set; }
        public GisAddon GisAddon { get; set; }
        public string Name { get; set; }
    }
}

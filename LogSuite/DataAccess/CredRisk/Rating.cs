namespace LogSuite.DataAccess.CredRisk
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Name { get; set; }
        public int RatingGroupId { get; set; }
        public RatingGroup RatingGroup { get; set; }
    }
}

namespace LogSuite.Shared.Models.CredRisk
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Name { get; set; }
        public RatingGroupDTO RatingGroup { get; set; }
    }
}

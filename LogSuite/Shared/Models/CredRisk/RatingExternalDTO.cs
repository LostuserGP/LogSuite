using System;

namespace LogSuite.Shared.Models.CredRisk
{
    public class RatingExternalDTO
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public int CounterpartyId { get; set; }
        public CounterpartyDTO Counterparty { get; set; }
        public int RatingAgencyId { get; set; }
        public RatingAgencyDTO RatingAgency { get; set; }
        public int RatingId { get; set; }
        public RatingDTO Rating { get; set; }
    }
}

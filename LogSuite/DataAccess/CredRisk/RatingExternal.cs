using System;

namespace LogSuite.DataAccess.CredRisk
{
    public class RatingExternal
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public int CounterpartyId { get; set; }
        public Counterparty Counterparty { get; set; }
        public int RatingAgencyId { get; set; }
        public RatingAgency RatingAgency { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
    }
}

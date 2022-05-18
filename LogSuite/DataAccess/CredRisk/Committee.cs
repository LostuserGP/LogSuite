using System;

namespace LogSuite.DataAccess.CredRisk
{
    public class Committee
    {
        public int Id { get; set; }
        public int CounterpartyId { get; set; }
        public Counterparty Counterparty { get; set; }
        public int CommitteeStatusId { get; set; }
        public CommitteeStatus CommitteeStatus { get; set; }
        public int? CommitteeLimitId { get; set; }
        public CommitteeLimit CommitteeLimit { get; set; }
        public DateTime DateStart { get; set; }
        public string Comment { get; set; }
    }
}

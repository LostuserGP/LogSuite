using System;

namespace LogSuite.Shared.Models.CredRisk
{
    public class CommitteeDTO
    {
        public int Id { get; set; }
        public int CounterpartyId { get; set; }
        public CounterpartyDTO Counterparty { get; set; }
        public int CommitteeStatusId { get; set; }
        public CommitteeStatusDTO CommitteeStatus { get; set; }
        public int CommitteeLimitId { get; set; }
        public CommitteeLimitDTO CommitteeLimit { get; set; }
        public DateTime DateStart { get; set; }
        public string Comment { get; set; }
    }
}

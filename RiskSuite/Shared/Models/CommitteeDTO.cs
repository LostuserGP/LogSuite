using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSuite.Shared.Models
{
    public class CommitteeDTO
    {
        public int Id { get; set; }
        public CounterpartyDTO Counterparty { get; set; }
        public CommitteeStatusDTO CommitteeStatus { get; set; }
        public CommitteeLimitDTO CommitteeLimit { get; set; }
        public DateTime DateStart { get; set; }
        public string Comment { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlCommitteeVoting
    {
        public int? CommitteeId { get; set; }
        public int? CommitteeMemberId { get; set; }
        public int? VotingResultId { get; set; }
        public string VotingDate { get; set; }
        public string Comments { get; set; }
    }
}

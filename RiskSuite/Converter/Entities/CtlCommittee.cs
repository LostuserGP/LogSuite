using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlCommittee
    {
        public int Id { get; set; }
        public int? CounterpartyId { get; set; }
        public int? StatusId { get; set; }
        public int? LimitationsId { get; set; }
        public string StartDate { get; set; }
        public string Comments { get; set; }
        public int? GroupId { get; set; }
    }
}

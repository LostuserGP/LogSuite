using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboVotingPerson
    {
        public string Id { get; set; }
        public string Person { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Orders { get; set; }
        public string PersonEng { get; set; }
        public string Position { get; set; }
        public string PositionEng { get; set; }
    }
}

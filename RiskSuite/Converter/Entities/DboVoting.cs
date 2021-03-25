using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboVoting
    {
        public string Id { get; set; }
        public string Committee { get; set; }
        public string Partner { get; set; }
        public DateTime? DateOfVoting { get; set; }
        public string Person { get; set; }
        public string Result { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string Limitation { get; set; }
        public string Note { get; set; }
        public string Agenda { get; set; }
    }
}

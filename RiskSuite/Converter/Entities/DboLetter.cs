using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboLetter
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public DateTime? LetterDate { get; set; }
        public string LetterNumber { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string Worker { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string DhlNumber { get; set; }
        public string Rus { get; set; }
        public string IsOfficial { get; set; }
        public string ReportSource { get; set; }
    }
}

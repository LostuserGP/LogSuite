using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboSrk
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Admdate { get; set; }
        public string Admuser { get; set; }
        public string Commitee { get; set; }
        public string Limitation { get; set; }
        public string Activity { get; set; }
        public string Result { get; set; }
        public string Note { get; set; }
        public string Agenda { get; set; }
    }
}

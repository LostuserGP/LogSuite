using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class DboLimitPartnerTable
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public DateTime? ContractDate { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? GetDate { get; set; }
        public string Admuser { get; set; }
        public DateTime? Admdate { get; set; }
    }
}

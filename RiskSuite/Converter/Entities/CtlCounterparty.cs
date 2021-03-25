using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlCounterparty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string BloombergTicker { get; set; }
        public int? SectorId { get; set; }
        public int? CountryOfDomicileId { get; set; }
        public int? CountryOfRiskId { get; set; }
        public int? Sapid { get; set; }
        public bool IntraGroup { get; set; }
        public string StartDate { get; set; }
        public string Comments { get; set; }
        public bool ToBeMonitored { get; set; }
        public int? PortfolioId { get; set; }
        public bool Longterm { get; set; }
        public bool Etp { get; set; }
        public bool Efet { get; set; }
        public string Gtc { get; set; }
    }
}

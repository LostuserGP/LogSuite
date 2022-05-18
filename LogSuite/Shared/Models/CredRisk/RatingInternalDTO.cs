using System;

namespace LogSuite.Shared.Models.CredRisk
{
    public class RatingInternalDTO
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public bool IsConservative { get; set; }
        public string Analyst { get; set; }
        public string Comment { get; set; }
        public int CounterpartyId { get; set; }
        public CounterpartyDTO Counterparty { get; set; }
        public int RatingId { get; set; }
        public RatingDTO Rating { get; set; }
        public int? RatingWcId { get; set; }
        public RatingDTO RatingWc { get; set; }
        public int? RiskClassId { get; set; }
        public RiskClassDTO RiskClass { get; set; }
        public int? FinancialStatementId { get; set; }
        public FinancialStatementDTO FinancialStatement { get; set; }
    }
}

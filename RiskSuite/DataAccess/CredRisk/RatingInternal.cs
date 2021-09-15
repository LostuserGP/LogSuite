using System;
using System.Collections.Generic;
using System.Text;

namespace LogSuite.DataAccess.CredRisk
{
    public class RatingInternal
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public bool IsConservative { get; set; }
        public string Analyst { get; set; }
        public string Comment { get; set; }
        public int CounterpartyId { get; set; }
        public Counterparty Counterparty { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public int? RatingWcId { get; set; }
        public Rating RatingWc { get; set; }
        public int? RiskClassId { get; set; }
        public RiskClass RiskClass { get; set; }
        public int? FinancialStatementId { get; set; }
        public FinancialStatement FinancialStatement { get; set; }

    }
}

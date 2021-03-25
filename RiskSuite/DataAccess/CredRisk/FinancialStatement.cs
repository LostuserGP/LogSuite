using System;
using System.Collections.Generic;
using System.Text;

namespace RiskSuite.DataAccess.CredRisk
{
    public class FinancialStatement
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public string Comment { get; set; }
        public int CounterpartyId { get; set; }
        public Counterparty Counterparty { get; set; }
        public int FinancialStatementStandardId { get; set; }
        public FinancialStatementStandard FinancialStatementStandard { get; set; }
    }
}

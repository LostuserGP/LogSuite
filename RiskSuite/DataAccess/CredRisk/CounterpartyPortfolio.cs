using System;
using System.Collections.Generic;
using System.Text;

namespace RiskSuite.DataAccess.CredRisk
{
    public class CounterpartyPortfolio
    {
        public int CounterpartyId { get; set; }
        public Counterparty Counterparty { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

    }
}

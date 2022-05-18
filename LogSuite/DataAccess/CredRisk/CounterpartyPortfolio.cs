namespace LogSuite.DataAccess.CredRisk
{
    public class CounterpartyPortfolio
    {
        public int CounterpartyId { get; set; }
        public Counterparty Counterparty { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

    }
}

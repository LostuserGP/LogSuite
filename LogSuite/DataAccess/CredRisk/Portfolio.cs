using System.Collections.Generic;

namespace LogSuite.DataAccess.CredRisk
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public ICollection<CounterpartyPortfolio> CounterpartyPortfolios { get; set; }
    }
}

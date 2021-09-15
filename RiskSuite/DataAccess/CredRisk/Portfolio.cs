using System;
using System.Collections.Generic;
using System.Text;

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

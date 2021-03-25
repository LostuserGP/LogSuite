using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSuite.Shared.Models
{
    public class RatingInternalDTO
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public bool IsConservative { get; set; }
        public string Analyst { get; set; }
        public string Comment { get; set; }
        public CounterpartyDTO Counterparty { get; set; }
        public RatingDTO Rating { get; set; }
        public RatingDTO RatingWc { get; set; }
        public RiskClassDTO RiskClass { get; set; }
        public FinancialStatementDTO FinancialStatement { get; set; }
    }
}

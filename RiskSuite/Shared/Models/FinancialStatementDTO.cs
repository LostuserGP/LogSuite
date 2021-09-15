using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared.Models
{
    public class FinancialStatementDTO
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public string Comment { get; set; }
        public int CounterpartyId { get; set; }
        public CounterpartyDTO Counterparty { get; set; }
        public int FinancialStatementStandardId { get; set; }
        public FinancialStatementStandardDTO FinancialStatementStandard { get; set; }
    }
}

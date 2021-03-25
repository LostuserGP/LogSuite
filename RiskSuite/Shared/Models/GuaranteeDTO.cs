using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSuite.Shared.Models
{
    public class GuaranteeDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime DateStart { get; set; }
        public long AmountInitial { get; set; }
        public CurrencyDTO Currency { get; set; }
        public CounterpartyDTO Counterparty { get; set; }
        public CounterpartyDTO Guarantor { get; set; }
        public GuaranteeTypeDTO GuaranteeType { get; set; }
        public CounterpartyDTO Beneficiar { get; set; }
        public SubsidiaryDTO Subsidiary { get; set; }
        public string Comment { get; set; }
    }
}

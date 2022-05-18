using LogSuite.DataAccess.References;
using System;

namespace LogSuite.DataAccess.CredRisk
{
    public class Guarantee
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime DateStart { get; set; }
        public long AmountInitial { get; set; }
        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int CounterpartyId { get; set; }
        public Counterparty Counterparty { get; set; }
        public int GuarantorId { get; set; }
        public Counterparty Guarantor { get; set; }
        public int? GuaranteeTypeId { get; set; }
        public GuaranteeType GuaranteeType { get; set; }
        public int BeneficiarId { get; set; }
        public Counterparty Beneficiar { get; set; }
        public int SubsidiaryId { get; set; }
        public Subsidiary Subsidiary { get; set; }
        public string Comment { get; set; }

    }
}

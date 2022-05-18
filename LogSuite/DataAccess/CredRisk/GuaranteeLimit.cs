using LogSuite.DataAccess.References;
using System;

namespace LogSuite.DataAccess.CredRisk
{
    public class GuaranteeLimit
    {
        public int Id { get; set; }
        public int GuarantorId { get; set; }
        public Counterparty Guarantor { get; set; }
        public DateTime DateAgreeStart { get; set; }
        public DateTime DateAgreeEnd { get; set; }
        public DateTime DateEnd { get; set; }
        public long Amount { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int GuaranteeTypeId { get; set; }
        public GuaranteeType GuaranteeType { get; set; }
    }
}

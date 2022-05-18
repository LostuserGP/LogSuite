using LogSuite.DataAccess.References;
using System;

namespace LogSuite.DataAccess.CredRisk
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public int CurrencyFromId { get; set; }
        public Currency CurrencyFrom { get; set; }
        public int CurrencyToId { get; set; }
        public Currency CurrencyTo { get; set; }
        public float Rate { get; set; }
        public DateTime DateStart { get; set; }
    }
}

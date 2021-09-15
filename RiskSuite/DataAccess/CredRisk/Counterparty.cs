using LogSuite.DataAccess.References;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogSuite.DataAccess.CredRisk
{
    public class Counterparty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsIntraGroup { get; set; }
        public DateTime DateStart { get; set; }
        public string Comment { get; set; }
        public bool IsMonitored { get; set; }
        public string Ticker { get; set; }
        public string Inn { get; set; }
        public string Swift { get; set; }
        public string BankClass { get; set; }
        public int? FinancialSectorId { get; set; }
        public FinancialSector FinancialSector { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public int? CountryRiskId { get; set; }
        public Country CountryRisk { get; set; }
        public int? RatingDonorId { get; set; }
        public Counterparty RatingDonor { get; set; }
        public int? CounterpartyGroupId { get; set; }
        public CounterpartyGroup CounterpartyGroup { get; set; }
        public string Gtc { get; set; }
        public bool IsLongTerm { get; set; }
        public bool IsEtp { get; set; }
        public bool IsEfet { get; set; }
        public string Causes { get; set; }
        public bool IsSrk { get; set; }
        public string Duns { get; set; }

        public ICollection<CounterpartyPortfolio> CounterpartyPortfolios { get; set; }
        public ICollection<Committee> Committees { get; set; }
        public ICollection<FinancialStatement> FinancialStatements { get; set; }
        public ICollection<Guarantee> Guarantees { get; set; }
        public ICollection<Guarantee> GuarantorGuarantees { get; set; }
        public ICollection<Guarantee> BeneficiarGuarantees { get; set; }
        public ICollection<RatingExternal> RatingExternals { get; set; }
        public ICollection<RatingInternal> RatingInternals { get; set; }
    }
}

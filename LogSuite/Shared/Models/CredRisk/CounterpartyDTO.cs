using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogSuite.Shared.Models.References;

namespace LogSuite.Shared.Models.CredRisk
{
    public class CounterpartyDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter counterparty name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter counterparty short name")]
        public string ShortName { get; set; }
        public bool IsIntraGroup { get; set; }
        public DateTime DateStart { get; set; }
        public string Comment { get; set; }
        public bool IsMonitored { get; set; }
        public string Ticker { get; set; }
        public string Inn { get; set; }
        public string Swift { get; set; }
        public string BankClass { get; set; }
        public FinancialSectorDTO FinancialSector { get; set; }
        public CountryDTO Country { get; set; }
        public CountryDTO CountryRisk { get; set; }
        public CounterpartyDTO RatingDonor { get; set; }
        public CounterpartyGroupDTO CounterpartyGroup { get; set; }
        public string Gtc { get; set; }
        public bool IsLongTerm { get; set; }
        public bool IsEtp { get; set; }
        public bool IsEfet { get; set; }
        public string Causes { get; set; }
        public bool IsSrk { get; set; }
        public string Duns { get; set; }

        public ICollection<CounterpartyPortfolioDTO> CounterpartyPortfolios { get; set; }
        public ICollection<CommitteeDTO> Committees { get; set; }
        public ICollection<FinancialStatementDTO> FinancialStatements { get; set; }
        public ICollection<GuaranteeDTO> Guarantees { get; set; }
        public ICollection<GuaranteeDTO> GuarantorGuarantees { get; set; }
        public ICollection<GuaranteeDTO> BeneficiarGuarantees { get; set; }
        public ICollection<RatingExternalDTO> RatingExternals { get; set; }
        public ICollection<RatingInternalDTO> RatingInternals { get; set; }
    }
}

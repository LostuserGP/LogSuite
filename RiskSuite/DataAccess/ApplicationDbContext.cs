using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LogSuite.DataAccess.CredRisk;
using System;
using System.Collections.Generic;
using System.Text;
using LogSuite.DataAccess.References;
using LogSuite.DataAccess.DailyReview;

namespace LogSuite.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        //public ApplicationDbContext(
        //    DbContextOptions<ApplicationDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        //{ }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        //public DbSet<ApplicationUser> Accounts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subsidiary> Subsidiaries { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<FinancialSector> FinancialSectors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryName> CountryNames { get; set; }
        public DbSet<CounterpartyGroup> CounterpartyGroups { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<CounterpartyPortfolio> CounterpartyPortfolios { get; set; }
        public DbSet<CommitteeStatus> CommitteeStatuses { get; set; }
        public DbSet<CommitteeLimit> CommitteeLimits { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<RatingGroup> RatingGroups { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RatingAgency> RatingAgencies { get; set; }
        public DbSet<RatingCountry> RatingCountries { get; set; }
        public DbSet<RiskClass> RiskClasses { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public DbSet<RatingExternal> RatingExternals { get; set; }
        public DbSet<FinancialStatementStandard> FinancialStatementStandards { get; set; }
        public DbSet<FinancialStatement> FinancialStatements { get; set; }
        public DbSet<RatingInternal> RatingInternals { get; set; }
        public DbSet<Guarantee> Guarantees { get; set; }
        public DbSet<GuaranteeReport> GuaranteeReports { get; set; }
        public DbSet<GuaranteeType> GuaranteeTypes { get; set; }
        public DbSet<GuaranteeLimit> GuaranteeLimits { get; set; }
        public DbSet<GuaranteeApprovalDocType> GuaranteeApprovalDocTypes { get; set; }
        public DbSet<GuaranteeApprovalDoc> GuaranteeApprovalDocs { get; set; }

        public DbSet<Gis> Gises { get; set; }
        public DbSet<GisInputName> GisInputNames { get; set; }
        public DbSet<GisOutputName> GisOutputNames { get; set; }
        public DbSet<GisInputValue> GisInputValues { get; set; }
        public DbSet<GisOutputValue> GisOutputValues { get; set; }
        public DbSet<GisAddon> GisAddons { get; set; }
        public DbSet<GisAddonName> GisAddonNames { get; set; }
        public DbSet<GisAddonValue> GisAddonValues { get; set; }
        public DbSet<GisCountry> GisCountries { get; set; }
        public DbSet<GisCountryResource> GisCountryResources { get; set; }
        public DbSet<GisCountryValue> GisCountryValues { get; set; }
        public DbSet<GisName> GisNames { get; set; }
        public DbSet<InputFileLog> InputFileLogs { get; set; }
        public DbSet<FileTypeSetting> FileTypeSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CounterpartyPortfolio>(cp =>
            {
                cp.HasKey(ur => new { ur.CounterpartyId, ur.PortfolioId });
                cp.HasOne(ur => ur.Portfolio)
                    .WithMany(r => r.CounterpartyPortfolios)
                    .HasForeignKey(ur => ur.PortfolioId)
                    .IsRequired();
                cp.HasOne(ur => ur.Counterparty)
                    .WithMany(r => r.CounterpartyPortfolios)
                    .HasForeignKey(ur => ur.CounterpartyId)
                    .IsRequired();
            });

            modelBuilder.Entity<Guarantee>(g =>
            {
                g.HasOne(u => u.Counterparty).WithMany(u => u.Guarantees).HasForeignKey(u => u.CounterpartyId);
                g.HasOne(u => u.Beneficiar).WithMany(u => u.BeneficiarGuarantees).HasForeignKey(u => u.BeneficiarId);
                g.HasOne(u => u.Guarantor).WithMany(u => u.GuarantorGuarantees).HasForeignKey(u => u.GuarantorId);
            });

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CredBaseContext : DbContext
    {
        public CredBaseContext()
        {
        }

        public CredBaseContext(DbContextOptions<CredBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDate> AccountDates { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Committee> Committees { get; set; }
        public virtual DbSet<CommitteeAgendum> CommitteeAgenda { get; set; }
        public virtual DbSet<CommitteeStatus> CommitteeStatuses { get; set; }
        public virtual DbSet<ContactsGe> ContactsGes { get; set; }
        public virtual DbSet<ContactsPartner> ContactsPartners { get; set; }
        public virtual DbSet<CorporateSecurity> CorporateSecurities { get; set; }
        public virtual DbSet<CountriesAgencyName> CountriesAgencyNames { get; set; }
        public virtual DbSet<CountriesRating> CountriesRatings { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountryLatestAndLowestRating> CountryLatestAndLowestRatings { get; set; }
        public virtual DbSet<CreditPartnerResult> CreditPartnerResults { get; set; }
        public virtual DbSet<CreditResult> CreditResults { get; set; }
        public virtual DbSet<CtlCommittee> CtlCommittees { get; set; }
        public virtual DbSet<CtlCommitteeLimitation> CtlCommitteeLimitations { get; set; }
        public virtual DbSet<CtlCommitteeMember> CtlCommitteeMembers { get; set; }
        public virtual DbSet<CtlCommitteeStatus> CtlCommitteeStatuses { get; set; }
        public virtual DbSet<CtlCommitteeVoting> CtlCommitteeVotings { get; set; }
        public virtual DbSet<CtlCorpSecConclusionType> CtlCorpSecConclusionTypes { get; set; }
        public virtual DbSet<CtlCorporateSecurityNote> CtlCorporateSecurityNotes { get; set; }
        public virtual DbSet<CtlCounterparty> CtlCounterparties { get; set; }
        public virtual DbSet<CtlCounterpartyGroup> CtlCounterpartyGroups { get; set; }
        public virtual DbSet<CtlCountry> CtlCountries { get; set; }
        public virtual DbSet<CtlCurrency> CtlCurrencies { get; set; }
        public virtual DbSet<CtlDepartment> CtlDepartments { get; set; }
        public virtual DbSet<CtlExchangeRate> CtlExchangeRates { get; set; }
        public virtual DbSet<CtlExternalRatingCounterparty> CtlExternalRatingCounterparties { get; set; }
        public virtual DbSet<CtlFinancialStatement> CtlFinancialStatements { get; set; }
        public virtual DbSet<CtlFrontOffice> CtlFrontOffices { get; set; }
        public virtual DbSet<CtlGroupLimit> CtlGroupLimits { get; set; }
        public virtual DbSet<CtlGuarantee> CtlGuarantees { get; set; }
        public virtual DbSet<CtlGuaranteeName> CtlGuaranteeNames { get; set; }
        public virtual DbSet<CtlGuaranteeType> CtlGuaranteeTypes { get; set; }
        public virtual DbSet<CtlGuaranteesLimit> CtlGuaranteesLimits { get; set; }
        public virtual DbSet<CtlGuaranteesReport> CtlGuaranteesReports { get; set; }
        public virtual DbSet<CtlInternalRatingCounterparty> CtlInternalRatingCounterparties { get; set; }
        public virtual DbSet<CtlOldName> CtlOldNames { get; set; }
        public virtual DbSet<CtlPortfolio> CtlPortfolios { get; set; }
        public virtual DbSet<CtlRatingAgency> CtlRatingAgencies { get; set; }
        public virtual DbSet<CtlRatingCountry> CtlRatingCountries { get; set; }
        public virtual DbSet<CtlRatingDonor> CtlRatingDonors { get; set; }
        public virtual DbSet<CtlRatingGroup> CtlRatingGroups { get; set; }
        public virtual DbSet<CtlRatingLimit> CtlRatingLimits { get; set; }
        public virtual DbSet<CtlRatingValue> CtlRatingValues { get; set; }
        public virtual DbSet<CtlRiskClass> CtlRiskClasses { get; set; }
        public virtual DbSet<CtlSector> CtlSectors { get; set; }
        public virtual DbSet<CtlVotingResult> CtlVotingResults { get; set; }
        public virtual DbSet<DboCurrency> DboCurrencies { get; set; }
        public virtual DbSet<DboCurrencyRate> DboCurrencyRates { get; set; }
        public virtual DbSet<DboExchangeRate> DboExchangeRates { get; set; }
        public virtual DbSet<DboFrontOffice> DboFrontOffices { get; set; }
        public virtual DbSet<DboGarantType> DboGarantTypes { get; set; }
        public virtual DbSet<DboGuarantee> DboGuarantees { get; set; }
        public virtual DbSet<DboLetter> DboLetters { get; set; }
        public virtual DbSet<DboLettersEntity> DboLettersEntities { get; set; }
        public virtual DbSet<DboLimitPartnerTable> DboLimitPartnerTables { get; set; }
        public virtual DbSet<DboMeasure> DboMeasures { get; set; }
        public virtual DbSet<DboPartner> DboPartners { get; set; }
        public virtual DbSet<DboPartnerAgencyName> DboPartnerAgencyNames { get; set; }
        public virtual DbSet<DboPartnerEntity> DboPartnerEntities { get; set; }
        public virtual DbSet<DboPartnerLimitation> DboPartnerLimitations { get; set; }
        public virtual DbSet<DboPartnerOwner> DboPartnerOwners { get; set; }
        public virtual DbSet<DboPartnerRatSuccesor> DboPartnerRatSuccesors { get; set; }
        public virtual DbSet<DboPartnerRating> DboPartnerRatings { get; set; }
        public virtual DbSet<DboPartnerReport> DboPartnerReports { get; set; }
        public virtual DbSet<DboPartnerRise> DboPartnerRises { get; set; }
        public virtual DbSet<DboPartnerTask> DboPartnerTasks { get; set; }
        public virtual DbSet<DboPartnerType> DboPartnerTypes { get; set; }
        public virtual DbSet<DboPfe> DboPves { get; set; }
        public virtual DbSet<DboPortfolio> DboPortfolios { get; set; }
        public virtual DbSet<DboRSource> DboRSources { get; set; }
        public virtual DbSet<DboRating> DboRatings { get; set; }
        public virtual DbSet<DboRatingAgency> DboRatingAgencies { get; set; }
        public virtual DbSet<DboRatingProcess> DboRatingProcesses { get; set; }
        public virtual DbSet<DboRatingsCorr> DboRatingsCorrs { get; set; }
        public virtual DbSet<DboRatingsScale> DboRatingsScales { get; set; }
        public virtual DbSet<DboRatingsType> DboRatingsTypes { get; set; }
        public virtual DbSet<DboRelation> DboRelations { get; set; }
        public virtual DbSet<DboReplyEntityStatus> DboReplyEntityStatuses { get; set; }
        public virtual DbSet<DboReplyStatus> DboReplyStatuses { get; set; }
        public virtual DbSet<DboReport> DboReports { get; set; }
        public virtual DbSet<DboReportSource> DboReportSources { get; set; }
        public virtual DbSet<DboReportType> DboReportTypes { get; set; }
        public virtual DbSet<DboReportWaiting> DboReportWaitings { get; set; }
        public virtual DbSet<DboRequestInf> DboRequestInfs { get; set; }
        public virtual DbSet<DboRiskClass> DboRiskClasses { get; set; }
        public virtual DbSet<DboSector> DboSectors { get; set; }
        public virtual DbSet<DboSrk> DboSrks { get; set; }
        public virtual DbSet<DboStage> DboStages { get; set; }
        public virtual DbSet<DboStandart> DboStandarts { get; set; }
        public virtual DbSet<DboState> DboStates { get; set; }
        public virtual DbSet<DboSysdiagram> DboSysdiagrams { get; set; }
        public virtual DbSet<DboTimePeriod> DboTimePeriods { get; set; }
        public virtual DbSet<DboUser> DboUsers { get; set; }
        public virtual DbSet<DboVoting> DboVotings { get; set; }
        public virtual DbSet<DboVotingPerson> DboVotingPersons { get; set; }
        public virtual DbSet<DboVotingResult> DboVotingResults { get; set; }
        public virtual DbSet<DboYN> DboYNs { get; set; }
        public virtual DbSet<DboYNNa> DboYNNas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=sql;Database=CredBase;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AccountDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCOUNT_DATES");

                entity.Property(e => e.AccountDate1)
                    .HasColumnType("datetime")
                    .HasColumnName("ACCOUNT_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCOUNT_TYPES");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACTIVITY");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Committee>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Committee");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.CommitteeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("COMMITTEE_DATE");

                entity.Property(e => e.CommitteeStatus)
                    .HasMaxLength(255)
                    .HasColumnName("COMMITTEE_STATUS");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.InPresence)
                    .HasMaxLength(255)
                    .HasColumnName("IN_PRESENCE");

                entity.Property(e => e.ProtocolNumber)
                    .HasMaxLength(10)
                    .HasColumnName("PROTOCOL_NUMBER");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<CommitteeAgendum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Committee_agenda");

                entity.Property(e => e.Agenda)
                    .HasMaxLength(150)
                    .HasColumnName("AGENDA");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<CommitteeStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Committee_status");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Status).HasMaxLength(100);
            });

            modelBuilder.Entity<ContactsGe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Contacts_GE");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_Date");

                entity.Property(e => e.FrontOffice)
                    .HasMaxLength(255)
                    .HasColumnName("Front_Office");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .HasColumnName("NOTE");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_Date");
            });

            modelBuilder.Entity<ContactsPartner>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Contacts_Partner");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.Adress).HasMaxLength(200);

                entity.Property(e => e.EMail)
                    .HasMaxLength(50)
                    .HasColumnName("E_MAIL");

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_Date");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsOfficial)
                    .HasMaxLength(255)
                    .HasColumnName("IS_Official");

                entity.Property(e => e.Male).HasMaxLength(255);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.Patronymic).HasMaxLength(20);

                entity.Property(e => e.Position).HasMaxLength(100);

                entity.Property(e => e.Rus)
                    .HasMaxLength(255)
                    .HasColumnName("RUS");

                entity.Property(e => e.SurName).HasMaxLength(20);

                entity.Property(e => e.SurNameDat)
                    .HasMaxLength(20)
                    .HasColumnName("SurName_Dat");

                entity.Property(e => e.Telephone).HasMaxLength(20);

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_Date");
            });

            modelBuilder.Entity<CorporateSecurity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Corporate_Security");

                entity.Property(e => e.Admdate).HasColumnType("datetime");

                entity.Property(e => e.Admuser).HasMaxLength(255);

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_Date");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsAdmit)
                    .HasMaxLength(255)
                    .HasColumnName("Is_Admit");

                entity.Property(e => e.Number).HasMaxLength(10);

                entity.Property(e => e.Partner).HasMaxLength(255);

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_Date");
            });

            modelBuilder.Entity<CountriesAgencyName>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("COUNTRIES_AGENCY_NAMES");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .HasColumnName("NAME");

                entity.Property(e => e.RatingAgency)
                    .HasMaxLength(255)
                    .HasColumnName("RATING_AGENCY");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<CountriesRating>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("COUNTRIES_RATINGS");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Pd).HasColumnName("PD");

                entity.Property(e => e.Rating)
                    .HasMaxLength(255)
                    .HasColumnName("RATING");

                entity.Property(e => e.RatingAgency)
                    .HasMaxLength(255)
                    .HasColumnName("RATING_AGENCY");

                entity.Property(e => e.RatingType)
                    .HasMaxLength(255)
                    .HasColumnName("RATING_TYPE");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("COUNTRIES");

                entity.Property(e => e.Country1)
                    .HasMaxLength(80)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<CountryLatestAndLowestRating>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CountryLatestAndLowestRating");

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Rating).HasMaxLength(30);

                entity.Property(e => e.RatingAgency).HasMaxLength(30);

                entity.Property(e => e.RatingId).HasColumnName("RatingID");
            });

            modelBuilder.Entity<CreditPartnerResult>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CREDIT_PARTNER_RESULTS");

                entity.Property(e => e.AdmDate).HasColumnType("datetime");

                entity.Property(e => e.AdmUser).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Measure).HasMaxLength(255);

                entity.Property(e => e.Partner).HasMaxLength(255);

                entity.Property(e => e.ReportId)
                    .HasMaxLength(255)
                    .HasColumnName("REPORT_ID");
            });

            modelBuilder.Entity<CreditResult>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CREDIT_RESULTS");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsCrisis)
                    .HasMaxLength(255)
                    .HasColumnName("IS_CRISIS");

                entity.Property(e => e.Measure)
                    .HasMaxLength(255)
                    .HasColumnName("MEASURE");

                entity.Property(e => e.ReportId)
                    .HasMaxLength(255)
                    .HasColumnName("REPORT_ID");

                entity.Property(e => e.TimePeriod)
                    .HasMaxLength(255)
                    .HasColumnName("TIME_PERIOD");
            });

            modelBuilder.Entity<CtlCommittee>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_Committees");

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LimitationsId).HasColumnName("LimitationsID");

                entity.Property(e => e.StartDate).HasMaxLength(10);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<CtlCommitteeLimitation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_CommitteeLimitations");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<CtlCommitteeMember>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_CommitteeMembers");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<CtlCommitteeStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_CommitteeStatuses");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<CtlCommitteeVoting>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_CommitteeVoting");

                entity.Property(e => e.CommitteeId).HasColumnName("CommitteeID");

                entity.Property(e => e.CommitteeMemberId).HasColumnName("CommitteeMemberID");

                entity.Property(e => e.VotingDate).HasMaxLength(10);

                entity.Property(e => e.VotingResultId).HasColumnName("VotingResultID");
            });

            modelBuilder.Entity<CtlCorpSecConclusionType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_CorpSecConclusionTypes");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<CtlCorporateSecurityNote>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_CorporateSecurityNote");

                entity.Property(e => e.ConclusionId).HasColumnName("ConclusionID");

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.Date).HasMaxLength(10);
            });

            modelBuilder.Entity<CtlCounterparty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_Counterparties");

                entity.Property(e => e.BloombergTicker).HasMaxLength(30);

                entity.Property(e => e.CountryOfDomicileId).HasColumnName("CountryOfDomicileID");

                entity.Property(e => e.CountryOfRiskId).HasColumnName("CountryOfRiskID");

                entity.Property(e => e.Gtc).HasMaxLength(20);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IntraGroup).HasColumnName("intraGroup");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");

                entity.Property(e => e.Sapid).HasColumnName("SAPID");

                entity.Property(e => e.SectorId).HasColumnName("SectorID");

                entity.Property(e => e.ShortName).HasMaxLength(15);

                entity.Property(e => e.StartDate).HasMaxLength(10);
            });

            modelBuilder.Entity<CtlCounterpartyGroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_CounterpartyGroup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<CtlCountry>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_Countries");

                entity.Property(e => e.BloombergTicker).HasMaxLength(30);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RussianName).HasMaxLength(50);

                entity.Property(e => e.ShortName).HasMaxLength(2);
            });

            modelBuilder.Entity<CtlCurrency>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_Currencies");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(3);
            });

            modelBuilder.Entity<CtlDepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_Departments");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<CtlExchangeRate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_ExchangeRates");

                entity.Property(e => e.ReportDate).HasMaxLength(10);
            });

            modelBuilder.Entity<CtlExternalRatingCounterparty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_ExternalRatingCounterparty");

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.RatingAgencyId).HasColumnName("RatingAgencyID");

                entity.Property(e => e.RatingValueId).HasColumnName("RatingValueID");

                entity.Property(e => e.StartDate).HasMaxLength(10);
            });

            modelBuilder.Entity<CtlFinancialStatement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_FinancialStatements");

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.Date).HasMaxLength(10);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Standards).HasMaxLength(100);
            });

            modelBuilder.Entity<CtlFrontOffice>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_FrontOffice");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<CtlGroupLimit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_GroupLimits");

                entity.Property(e => e.BankLimit).HasMaxLength(255);

                entity.Property(e => e.GroupLimit).HasMaxLength(255);

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<CtlGuarantee>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_Guarantees");

                entity.Property(e => e.Amount).HasMaxLength(255);

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.EndDate).HasMaxLength(10);

                entity.Property(e => e.GuaranteeNumber).HasMaxLength(100);

                entity.Property(e => e.GuarantorId).HasColumnName("GuarantorID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StartDate).HasMaxLength(10);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<CtlGuaranteeName>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_GuaranteeNames");

                entity.Property(e => e.GuaranteeName).HasMaxLength(100);

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<CtlGuaranteeType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_GuaranteeTypes");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<CtlGuaranteesLimit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_GuaranteesLimits");

                entity.Property(e => e.AgreeEndDate).HasMaxLength(10);

                entity.Property(e => e.AgreeFirstDate).HasMaxLength(10);

                entity.Property(e => e.Amount).HasMaxLength(255);

                entity.Property(e => e.BankId).HasColumnName("BankID");

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.GuaranteeEndDate).HasMaxLength(10);

                entity.Property(e => e.GuaranteeNameId).HasColumnName("GuaranteeNameID");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<CtlGuaranteesReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_GuaranteesReports");

                entity.Property(e => e.ApprovalDate).HasMaxLength(10);

                entity.Property(e => e.ApprovalNumber).HasMaxLength(100);

                entity.Property(e => e.ApprovalType).HasMaxLength(100);

                entity.Property(e => e.BankId).HasColumnName("BankID");

                entity.Property(e => e.BenificiaryName).HasMaxLength(100);

                entity.Property(e => e.ChangeAmount).HasMaxLength(255);

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.DaughterId).HasColumnName("DaughterID");

                entity.Property(e => e.EndMonthAmount).HasMaxLength(255);

                entity.Property(e => e.FinishDate).HasMaxLength(10);

                entity.Property(e => e.FirstAmount).HasMaxLength(255);

                entity.Property(e => e.GuaranteeType).HasMaxLength(100);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Note).HasMaxLength(100);

                entity.Property(e => e.Number).HasMaxLength(100);

                entity.Property(e => e.OperationAmount).HasMaxLength(255);

                entity.Property(e => e.ReportDate).HasMaxLength(10);

                entity.Property(e => e.StartDate).HasMaxLength(10);

                entity.Property(e => e.StartMonthAmount).HasMaxLength(255);
            });

            modelBuilder.Entity<CtlInternalRatingCounterparty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_InternalRatingCounterparty");

                entity.Property(e => e.Analyst).HasMaxLength(30);

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.FinancialStatementId).HasColumnName("FinancialStatementID");

                entity.Property(e => e.RatingValueId).HasColumnName("RatingValueID");

                entity.Property(e => e.RatingValueWithoutCountryId).HasColumnName("RatingValueWithoutCountryID");

                entity.Property(e => e.RiskClassId).HasColumnName("RiskClassID");

                entity.Property(e => e.StartDate).HasMaxLength(10);
            });

            modelBuilder.Entity<CtlOldName>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_OldNames");

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.OldName).HasMaxLength(50);
            });

            modelBuilder.Entity<CtlPortfolio>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_Portfolios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<CtlRatingAgency>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_RatingAgencies");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<CtlRatingCountry>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_RatingCountry");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.RatingAgencyId).HasColumnName("RatingAgencyID");

                entity.Property(e => e.RatingValueId).HasColumnName("RatingValueID");

                entity.Property(e => e.StartDate).HasMaxLength(10);
            });

            modelBuilder.Entity<CtlRatingDonor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_RatingDonor");

                entity.Property(e => e.CounterpartyId).HasColumnName("CounterpartyID");

                entity.Property(e => e.DonorId).HasColumnName("DonorID");
            });

            modelBuilder.Entity<CtlRatingGroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_RatingGroups");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RatingId).HasColumnName("RatingID");
            });

            modelBuilder.Entity<CtlRatingLimit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_RatingLimits");

                entity.Property(e => e.BankLimit).HasMaxLength(255);

                entity.Property(e => e.GroupLimit).HasMaxLength(255);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RatingId).HasColumnName("RatingID");
            });

            modelBuilder.Entity<CtlRatingValue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_RatingValues");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<CtlRiskClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_RiskClasses");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(3);
            });

            modelBuilder.Entity<CtlSector>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_Sectors");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RussianName).HasMaxLength(50);

                entity.Property(e => e.SectorName).HasMaxLength(30);
            });

            modelBuilder.Entity<CtlVotingResult>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ctl_VotingResults");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<DboCurrency>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_CURRENCIES");

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .HasColumnName("CURRENCY");

                entity.Property(e => e.CurrencyRus)
                    .HasMaxLength(50)
                    .HasColumnName("CURRENCY_RUS");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<DboCurrencyRate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Currency_Rates");

                entity.Property(e => e.AccountDate)
                    .HasMaxLength(255)
                    .HasColumnName("Account_Date");

                entity.Property(e => e.Activity).HasMaxLength(255);

                entity.Property(e => e.CurrencyBase)
                    .HasMaxLength(255)
                    .HasColumnName("Currency_Base");

                entity.Property(e => e.CurrencyNeed)
                    .HasMaxLength(255)
                    .HasColumnName("Currency_Need");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_date");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_date");
            });

            modelBuilder.Entity<DboExchangeRate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_exchange_rates");

                entity.Property(e => e.FromCurrency)
                    .HasMaxLength(3)
                    .HasColumnName("from_currency");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.ReportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("report_date");

                entity.Property(e => e.ToCurrency)
                    .HasMaxLength(3)
                    .HasColumnName("to_currency");
            });

            modelBuilder.Entity<DboFrontOffice>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Front_Office");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Person).HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(10);
            });

            modelBuilder.Entity<DboGarantType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_GARANT_TYPES");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Type)
                    .HasMaxLength(80)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<DboGuarantee>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_GUARANTEES");

                entity.Property(e => e.AdmDate).HasColumnType("datetime");

                entity.Property(e => e.AdmUser).HasMaxLength(255);

                entity.Property(e => e.Bank)
                    .HasMaxLength(255)
                    .HasColumnName("BANK");

                entity.Property(e => e.Company)
                    .HasMaxLength(255)
                    .HasColumnName("COMPANY");

                entity.Property(e => e.Currency)
                    .HasMaxLength(255)
                    .HasColumnName("CURRENCY");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.GarantSum).HasColumnName("GARANT_SUM");

                entity.Property(e => e.GarantType)
                    .HasMaxLength(255)
                    .HasColumnName("GARANT_TYPE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboLetter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Letters");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.DhlNumber)
                    .HasMaxLength(20)
                    .HasColumnName("DHL_Number");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsOfficial)
                    .HasMaxLength(255)
                    .HasColumnName("IS_Official");

                entity.Property(e => e.LetterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Letter_date");

                entity.Property(e => e.LetterNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Letter_Number");

                entity.Property(e => e.Note).HasMaxLength(100);

                entity.Property(e => e.Partner).HasMaxLength(255);

                entity.Property(e => e.ReportSource)
                    .HasMaxLength(255)
                    .HasColumnName("Report_Source");

                entity.Property(e => e.Rus)
                    .HasMaxLength(255)
                    .HasColumnName("RUS");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e.Worker).HasMaxLength(255);
            });

            modelBuilder.Entity<DboLettersEntity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Letters_Entity");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Information).HasMaxLength(255);

                entity.Property(e => e.LetterId)
                    .HasMaxLength(255)
                    .HasColumnName("Letter_Id");

                entity.Property(e => e.ReportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Report_Date");

                entity.Property(e => e.ReportType)
                    .HasMaxLength(255)
                    .HasColumnName("Report_Type");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<DboLimitPartnerTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Limit_Partner_Table");

                entity.Property(e => e.Admdate).HasColumnType("datetime");

                entity.Property(e => e.Admuser).HasMaxLength(255);

                entity.Property(e => e.ContractDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Contract_Date");

                entity.Property(e => e.ContractNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Contract_Number");

                entity.Property(e => e.GetDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Get_Date");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Partner).HasMaxLength(255);
            });

            modelBuilder.Entity<DboMeasure>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Measure");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Measure)
                    .HasMaxLength(100)
                    .HasColumnName("MEASURE");
            });

            modelBuilder.Entity<DboPartner>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_PARTNERS");

                entity.Property(e => e.ActCountry)
                    .HasMaxLength(255)
                    .HasColumnName("ACT_COUNTRY");

                entity.Property(e => e.Activity)
                    .HasMaxLength(255)
                    .HasColumnName("ACTIVITY");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("FULL_NAME");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsLimitByCountry)
                    .HasMaxLength(255)
                    .HasColumnName("Is_Limit_By_Country");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.Relation)
                    .HasMaxLength(255)
                    .HasColumnName("RELATION");

                entity.Property(e => e.Sector)
                    .HasMaxLength(255)
                    .HasColumnName("SECTOR");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboPartnerAgencyName>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_PARTNER_AGENCY_NAMES");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .HasColumnName("NAME");

                entity.Property(e => e.Partner).HasMaxLength(255);

                entity.Property(e => e.RatingAgency)
                    .HasMaxLength(255)
                    .HasColumnName("RATING_AGENCY");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboPartnerEntity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_PARTNER_ENTITY");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(100)
                    .HasColumnName("SHORT_NAME");
            });

            modelBuilder.Entity<DboPartnerLimitation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Partner_Limitations");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Limitation)
                    .HasMaxLength(100)
                    .HasColumnName("LIMITATION");

                entity.Property(e => e.Orders)
                    .HasMaxLength(255)
                    .HasColumnName("ORDERS");

                entity.Property(e => e.PrintLimitation).HasColumnName("PRINT_LIMITATION");
            });

            modelBuilder.Entity<DboPartnerOwner>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Partner_Owners");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.CountryOwner)
                    .HasMaxLength(255)
                    .HasColumnName("COUNTRY_OWNER");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Interest)
                    .HasColumnType("decimal(18, 17)")
                    .HasColumnName("INTEREST");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.PartnerOwner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER_OWNER");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboPartnerRatSuccesor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_PARTNER_RAT_SUCCESOR");

                entity.Property(e => e.AdmDate).HasColumnType("datetime");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Note)
                    .HasMaxLength(100)
                    .HasColumnName("NOTE");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.RatingPartner)
                    .HasMaxLength(255)
                    .HasColumnName("RATING_PARTNER");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboPartnerRating>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_PARTNER_RATINGS");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsConservative)
                    .HasMaxLength(255)
                    .HasColumnName("IS_CONSERVATIVE");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.Pd).HasColumnName("PD");

                entity.Property(e => e.Rating)
                    .HasMaxLength(255)
                    .HasColumnName("RATING");

                entity.Property(e => e.RatingAgency)
                    .HasMaxLength(255)
                    .HasColumnName("RATING_AGENCY");

                entity.Property(e => e.RatingProcess)
                    .HasMaxLength(255)
                    .HasColumnName("RATING_PROCESS");

                entity.Property(e => e.RatingWithoutCountry)
                    .HasMaxLength(255)
                    .HasColumnName("RATING_WITHOUT_COUNTRY");

                entity.Property(e => e.RiskClass)
                    .HasMaxLength(255)
                    .HasColumnName("RISK_CLASS");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboPartnerReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Partner_Reports");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.GetDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Get_Date");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsAnnex)
                    .HasMaxLength(255)
                    .HasColumnName("IS_Annex");

                entity.Property(e => e.IsRated)
                    .HasMaxLength(255)
                    .HasColumnName("IS_RATED");

                entity.Property(e => e.LetterId)
                    .HasMaxLength(255)
                    .HasColumnName("LETTER_ID");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.ReportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("REPORT_DATE");

                entity.Property(e => e.ReportSource)
                    .HasMaxLength(255)
                    .HasColumnName("REPORT_SOURCE");

                entity.Property(e => e.ReportType)
                    .HasMaxLength(255)
                    .HasColumnName("REPORT_TYPE");

                entity.Property(e => e.Standarts).HasMaxLength(255);
            });

            modelBuilder.Entity<DboPartnerRise>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Partner_Rise");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.CoFromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CO_From_date");

                entity.Property(e => e.CoToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CO_To_date");

                entity.Property(e => e.CoWorker)
                    .HasMaxLength(255)
                    .HasColumnName("CO_Worker");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsActual)
                    .HasMaxLength(255)
                    .HasColumnName("IS_Actual");

                entity.Property(e => e.Partner).HasMaxLength(255);

                entity.Property(e => e.PartnerTask)
                    .HasMaxLength(255)
                    .HasColumnName("Partner_Task");

                entity.Property(e => e.Portfolio)
                    .HasMaxLength(255)
                    .HasColumnName("PORTFOLIO");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.Worker).HasMaxLength(255);
            });

            modelBuilder.Entity<DboPartnerTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Partner_Task");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Task)
                    .HasMaxLength(50)
                    .HasColumnName("TASK");
            });

            modelBuilder.Entity<DboPartnerType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_PARTNER_TYPE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<DboPfe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_PFE");

                entity.Property(e => e.AccountDate)
                    .HasMaxLength(255)
                    .HasColumnName("ACCOUNT_DATE");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(255)
                    .HasColumnName("ACCOUNT_TYPE");

                entity.Property(e => e.Activity).HasMaxLength(255);

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.Currency).HasMaxLength(255);

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Partner).HasMaxLength(255);

                entity.Property(e => e.Pfe).HasColumnName("PFE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("START_DATE");

                entity.Property(e => e.TimePeriod)
                    .HasMaxLength(255)
                    .HasColumnName("TIME_PERIOD");
            });

            modelBuilder.Entity<DboPortfolio>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Portfolio");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<DboRSource>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_R_SOURCES");

                entity.Property(e => e.Activity)
                    .HasMaxLength(255)
                    .HasColumnName("ACTIVITY");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.EaDformula).HasColumnName("EaDFormula");

                entity.Property(e => e.EaDref).HasMaxLength(100);

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.PartnerType)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER_TYPE");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboRating>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_RATINGS");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Rating)
                    .HasMaxLength(50)
                    .HasColumnName("RATING");
            });

            modelBuilder.Entity<DboRatingAgency>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_RATING_AGENCIES");

                entity.Property(e => e.Agency)
                    .HasMaxLength(50)
                    .HasColumnName("AGENCY");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<DboRatingProcess>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Rating_Process");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.EndDrat)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Drat");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_date");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Partner).HasMaxLength(255);

                entity.Property(e => e.PartnerReport)
                    .HasMaxLength(255)
                    .HasColumnName("Partner_report");

                entity.Property(e => e.Stage).HasMaxLength(255);

                entity.Property(e => e.StartDrat)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Drat");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_date");

                entity.Property(e => e.Worker).HasMaxLength(255);
            });

            modelBuilder.Entity<DboRatingsCorr>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_RATINGS_CORR");

                entity.Property(e => e.Correlation).HasColumnName("CORRELATION");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Rat1)
                    .HasMaxLength(255)
                    .HasColumnName("RAT1");

                entity.Property(e => e.Rat2)
                    .HasMaxLength(255)
                    .HasColumnName("RAT2");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboRatingsScale>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_RATINGS_SCALE");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsCrisis)
                    .HasMaxLength(255)
                    .HasColumnName("IS_CRISIS");

                entity.Property(e => e.Pd).HasColumnName("PD");

                entity.Property(e => e.Rating)
                    .HasMaxLength(255)
                    .HasColumnName("RATING");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboRatingsType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_RATINGS_TYPE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.RatingType)
                    .HasMaxLength(50)
                    .HasColumnName("RATING_TYPE");
            });

            modelBuilder.Entity<DboRelation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_RELATION");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Relation).HasMaxLength(100);
            });

            modelBuilder.Entity<DboReplyEntityStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_ReplyEntity_Status");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<DboReplyStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Reply_Status");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Status).HasMaxLength(100);
            });

            modelBuilder.Entity<DboReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Reports");

                entity.Property(e => e.AccountDate)
                    .HasMaxLength(255)
                    .HasColumnName("ACCOUNT_DATE");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(255)
                    .HasColumnName("ACCOUNT_TYPE");

                entity.Property(e => e.Activity)
                    .HasMaxLength(255)
                    .HasColumnName("ACTIVITY");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.CountDays)
                    .HasMaxLength(255)
                    .HasColumnName("COUNT_DAYS");

                entity.Property(e => e.Currency)
                    .HasMaxLength(255)
                    .HasColumnName("CURRENCY");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.IsCrisis)
                    .HasMaxLength(255)
                    .HasColumnName("IS_CRISIS");

                entity.Property(e => e.IsProcess)
                    .HasMaxLength(255)
                    .HasColumnName("IS_PROCESS");

                entity.Property(e => e.Probability).HasColumnName("PROBABILITY");

                entity.Property(e => e.Simulation)
                    .HasMaxLength(255)
                    .HasColumnName("SIMULATION");

                entity.Property(e => e.TimePeriod)
                    .HasMaxLength(255)
                    .HasColumnName("TIME_PERIOD");
            });

            modelBuilder.Entity<DboReportSource>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Report_Sources");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Source).HasMaxLength(50);
            });

            modelBuilder.Entity<DboReportType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_REPORT_TYPES");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.ReportType)
                    .HasMaxLength(50)
                    .HasColumnName("REPORT_TYPE");
            });

            modelBuilder.Entity<DboReportWaiting>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Report_waiting");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_date");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Partner).HasMaxLength(255);

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_date");

                entity.Property(e => e.WaitingPeriod).HasColumnName("Waiting_period");
            });

            modelBuilder.Entity<DboRequestInf>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Request_Inf");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Information).HasMaxLength(100);
            });

            modelBuilder.Entity<DboRiskClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_RISK_CLASS");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.RiskClass)
                    .HasMaxLength(50)
                    .HasColumnName("RISK_CLASS");
            });

            modelBuilder.Entity<DboSector>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_SECTOR");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<DboSrk>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_SRK");

                entity.Property(e => e.Activity)
                    .HasMaxLength(255)
                    .HasColumnName("ACTIVITY");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.Agenda)
                    .HasMaxLength(255)
                    .HasColumnName("AGENDA");

                entity.Property(e => e.Commitee)
                    .HasMaxLength(255)
                    .HasColumnName("COMMITEE");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Limitation)
                    .HasMaxLength(255)
                    .HasColumnName("LIMITATION");

                entity.Property(e => e.Note)
                    .HasMaxLength(100)
                    .HasColumnName("NOTE");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.Result)
                    .HasMaxLength(255)
                    .HasColumnName("RESULT");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TO_DATE");
            });

            modelBuilder.Entity<DboStage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Stage");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Stage)
                    .HasMaxLength(50)
                    .HasColumnName("STAGE");
            });

            modelBuilder.Entity<DboStandart>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Standarts");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Standart).HasMaxLength(100);
            });

            modelBuilder.Entity<DboState>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_STATE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .HasColumnName("STATE");
            });

            modelBuilder.Entity<DboSysdiagram>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_sysdiagrams");

                entity.Property(e => e.Definition)
                    .HasColumnType("image")
                    .HasColumnName("definition");

                entity.Property(e => e.DiagramId).HasColumnName("diagram_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.PrincipalId).HasColumnName("principal_id");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<DboTimePeriod>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_TIME_PERIODS");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.TimePeriod)
                    .HasMaxLength(100)
                    .HasColumnName("TIME_PERIOD");
            });

            modelBuilder.Entity<DboUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Users");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_Date");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("FULL_NAME");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Telephone).HasMaxLength(20);

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_Date");

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            modelBuilder.Entity<DboVoting>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Voting");

                entity.Property(e => e.Admdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADMDATE");

                entity.Property(e => e.Admuser)
                    .HasMaxLength(255)
                    .HasColumnName("ADMUSER");

                entity.Property(e => e.Agenda)
                    .HasMaxLength(255)
                    .HasColumnName("AGENDA");

                entity.Property(e => e.Committee)
                    .HasMaxLength(255)
                    .HasColumnName("COMMITTEE");

                entity.Property(e => e.DateOfVoting).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Limitation)
                    .HasMaxLength(255)
                    .HasColumnName("LIMITATION");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .HasColumnName("NOTE");

                entity.Property(e => e.Partner)
                    .HasMaxLength(255)
                    .HasColumnName("PARTNER");

                entity.Property(e => e.Person).HasMaxLength(255);

                entity.Property(e => e.Result).HasMaxLength(255);
            });

            modelBuilder.Entity<DboVotingPerson>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Voting_Persons");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_date");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Orders)
                    .HasMaxLength(255)
                    .HasColumnName("ORDERS");

                entity.Property(e => e.Person).HasMaxLength(50);

                entity.Property(e => e.PersonEng)
                    .HasMaxLength(50)
                    .HasColumnName("Person_eng");

                entity.Property(e => e.Position).HasMaxLength(200);

                entity.Property(e => e.PositionEng)
                    .HasMaxLength(200)
                    .HasColumnName("Position_eng");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_Date");
            });

            modelBuilder.Entity<DboVotingResult>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Voting_Results");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Result).HasMaxLength(50);
            });

            modelBuilder.Entity<DboYN>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Y_N");

                entity.Property(e => e.Answer)
                    .HasMaxLength(50)
                    .HasColumnName("ANSWER");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<DboYNNa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_Y_N_NA");

                entity.Property(e => e.Answer)
                    .HasMaxLength(50)
                    .HasColumnName("ANSWER");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

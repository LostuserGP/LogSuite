using AutoMapper;
using RiskSuite.DataAccess;
using RiskSuite.DataAccess.CredRisk;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<FinancialSector, FinancialSectorDTO>().ReverseMap();
            CreateMap<CounterpartyGroup, CounterpartyGroupDTO>().ReverseMap();
            CreateMap<CommitteeStatus, CommitteeStatusDTO>().ReverseMap();
            CreateMap<CommitteeLimit, CommitteeLimitDTO>().ReverseMap();
            CreateMap<Committee, CommitteeDTO>().ReverseMap();
            CreateMap<FinancialStatementStandard, FinancialStatementStandardDTO>().ReverseMap();
            CreateMap<FinancialStatement, FinancialStatementDTO>().ReverseMap();
            CreateMap<Currency, CurrencyDTO>().ReverseMap();
            CreateMap<GuaranteeType, GuaranteeTypeDTO>().ReverseMap();
            CreateMap<Subsidiary, SubsidiaryDTO>().ReverseMap();
            CreateMap<Guarantee, GuaranteeDTO>().ReverseMap();
            CreateMap<RatingAgency, RatingAgencyDTO>().ReverseMap();
            CreateMap<RatingGroup, RatingGroupDTO>().ReverseMap();
            CreateMap<Rating, RatingDTO>().ReverseMap();
            CreateMap<RatingExternal, RatingExternalDTO>().ReverseMap();
            CreateMap<RiskClass, RiskClassDTO>().ReverseMap();
            CreateMap<RatingInternal, RatingInternalDTO>().ReverseMap();
            CreateMap<Counterparty, CounterpartyDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();

            CreateMap<CounterpartyPortfolio, CounterpartyPortfolioDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.PortfolioId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Portfolio.Name))
                .ForMember(dest => dest.NameEn, opts => opts.MapFrom(src => src.Portfolio.NameEn));
        }
    }
}

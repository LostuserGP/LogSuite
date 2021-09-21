using AutoMapper;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.DataAccess.DailyReview;
using LogSuite.DataAccess.References;
using LogSuite.Shared.Authorization;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.DailyReview;
using LogSuite.Shared.Models.References;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<CountryName, CountryNameDTO>().ReverseMap();
            CreateMap<FinancialSector, FinancialSectorDTO>().ReverseMap();
            CreateMap<CounterpartyGroup, CounterpartyGroupDTO>().ReverseMap();
            CreateMap<CommitteeStatus, CommitteeStatusDTO>().ReverseMap();
            CreateMap<CommitteeLimit, CommitteeLimitDTO>().ReverseMap();
            CreateMap<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>().ReverseMap();
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

            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Department, opts => opts.MapFrom(src => src.Department));
            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));

            CreateMap<InputFileLog, InputFileLogDTO>();
            CreateMap<InputFileLogDTO, InputFileLog>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Filename, opts => opts.MapFrom(src => src.Filename))
                .ForMember(dest => dest.TimeFile, opts => opts.MapFrom(src => src.TimeFile))
                .ForMember(dest => dest.TimeInput, opts => opts.MapFrom(src => src.TimeInput))
                .ForMember(dest => dest.DateFile, opts => opts.MapFrom(src => src.DateFile));

            CreateMap<Gis, GisDTO>().ReverseMap();
            CreateMap<GisAddon, GisAddonDTO>().ReverseMap();
            CreateMap<GisAddonName, GisAddonNameDTO>().ReverseMap();
            CreateMap<GisAddonValue, GisAddonValueDTO>().ReverseMap();
            CreateMap<GisCountry, GisCountryDTO>().ReverseMap();
            CreateMap<GisCountryResource, GisCountryResourceDTO>().ReverseMap();
            CreateMap<GisCountryValue, GisCountryValueDTO>().ReverseMap();
            CreateMap<GisName, GisNameDTO>().ReverseMap();
            
            CreateMap<GisInputName, GisInputNameDTO>().ReverseMap();
            CreateMap<GisOutputName, GisOutputNameDTO>().ReverseMap();
            CreateMap<GisInputValue, GisInputValueDTO>().ReverseMap();
            CreateMap<GisOutputValue, GisOutputValueDTO>().ReverseMap();
            CreateMap<FileTypeSetting, FileTypeSettingDTO>().ReverseMap();
        }
    }
}

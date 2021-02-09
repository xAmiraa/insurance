using AutoMapper;
using InsuranceClaims.Core.Common;
using InsuranceClaims.DTO.Company.AttachmentType;
using InsuranceClaims.DTO.Company.Company;
using InsuranceClaims.DTO.Lookup.Country;
using InsuranceClaims.DTO.Lookup.Currency;
using InsuranceClaims.DTO.Lookup.IdentificationType;
using InsuranceClaims.DTO.Lookup.PolicyInsurer;
using InsuranceClaims.DTO.Security.User;

namespace InsuranceClaims.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Security
            CreateMap<Data.DbModels.SecuritySchema.ApplicationUser, CreateUpdateUserDto>().ReverseMap();
            CreateMap<Data.DbModels.SecuritySchema.ApplicationUser, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap();
            #endregion

            #region Lookup
            CreateMap<Data.DbModels.LookupSchema.IdentificationType, CreateUpdateIdentificationTypeDto>().ReverseMap();
            CreateMap<Data.DbModels.LookupSchema.IdentificationType, IdentificationTypeDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ForMember(dest => dest.Updator, opt => opt.MapFrom(src => src.Updator == null ? "-" : $"{src.Updator.FirstName} {src.Updator.LastName}"))
                .ReverseMap();

            CreateMap<Data.DbModels.LookupSchema.PolicyInsurer, CreateUpdatePolicyInsurerDto>().ReverseMap();
            CreateMap<Data.DbModels.LookupSchema.PolicyInsurer, PolicyInsurerDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ForMember(dest => dest.Updator, opt => opt.MapFrom(src => src.Updator == null ? "-" : $"{src.Updator.FirstName} {src.Updator.LastName}"))
                .ReverseMap();

            CreateMap<Data.DbModels.LookupSchema.Country, CountryDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ForMember(dest => dest.Updator, opt => opt.MapFrom(src => src.Updator == null ? "-" : $"{src.Updator.FirstName} {src.Updator.LastName}"))
                .ReverseMap();

            CreateMap<Data.DbModels.LookupSchema.Currency, CurrencyDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ForMember(dest => dest.Updator, opt => opt.MapFrom(src => src.Updator == null ? "-" : $"{src.Updator.FirstName} {src.Updator.LastName}"))
                .ReverseMap();
            #endregion

            #region Company
            CreateMap<Data.DbModels.CompanySchema.Company, CreateCompanyDto>().ReverseMap();
            CreateMap<Data.DbModels.CompanySchema.Company, CompanyDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ForMember(dest => dest.Updator, opt => opt.MapFrom(src => src.Updator == null ? "-" : $"{src.Updator.FirstName} {src.Updator.LastName}"))
                .ReverseMap();

            CreateMap<Data.DbModels.CompanySchema.CompanyAttachmentType, CreateUpdateAttachmentTypeDto>().ReverseMap();
            CreateMap<Data.DbModels.CompanySchema.CompanyAttachmentType, AttachmentTypeDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ForMember(dest => dest.Updator, opt => opt.MapFrom(src => src.Updator == null ? "-" : $"{src.Updator.FirstName} {src.Updator.LastName}"))
                .ReverseMap();
            #endregion

            #region Viewing as Lookup
            CreateMap<Data.DbModels.SecuritySchema.ApplicationRole, LookupDto>().ReverseMap();

            // Lookup
            CreateMap<Data.DbModels.LookupSchema.IdentificationType, LookupDto>().ReverseMap();
            CreateMap<Data.DbModels.LookupSchema.PolicyInsurer, LookupDto>().ReverseMap();
            CreateMap<Data.DbModels.LookupSchema.Country, LookupDto>().ReverseMap();
            CreateMap<Data.DbModels.LookupSchema.Currency, LookupDto>().ReverseMap();

            // Company
            CreateMap<Data.DbModels.CompanySchema.CompanyAttachmentType, LookupDto>().ReverseMap();
            #endregion
        }
    }
}

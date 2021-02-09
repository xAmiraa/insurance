using Autofac;
using InsuranceClaims.Core.Common;
using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.Services.Company.AttachmentType;
using InsuranceClaims.Services.Company.Company;
using InsuranceClaims.Services.Customer.Customer;
using InsuranceClaims.Services.Lookup.Country;
using InsuranceClaims.Services.Lookup.IdentificationType;
using InsuranceClaims.Services.Lookup.PolicyInsurer;
using InsuranceClaims.Services.Security.Account;
using InsuranceClaims.Services.SendEmail;
using InsuranceClaims.Services.UploadFiles;

namespace InsuranceClaims.Services
{
    public class AutoFacConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            #region Shared
            builder.RegisterType<ResponseDTO>().As<IResponseDTO>().InstancePerLifetimeScope();
            builder.RegisterType<UploadFilesService>().As<IUploadFilesService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailConfiguration>().As<IEmailConfiguration>().InstancePerLifetimeScope();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            #endregion

            #region Lookup
            builder.RegisterType<IdentificationTypeService>().As<IIdentificationTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<PolicyInsurerService>().As<IPolicyInsurerService>().InstancePerLifetimeScope();
            builder.RegisterType<CountryService>().As<ICountryService>().InstancePerLifetimeScope();
            #endregion

            #region Customer
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            #endregion

            #region Company
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerLifetimeScope();
            builder.RegisterType<AttachmentTypeService>().As<IAttachmentTypeService>().InstancePerLifetimeScope();
            #endregion

        }
    }
}
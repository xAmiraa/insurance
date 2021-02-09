using InsuranceClaims.Data.DbModels.ClaimSchema;
using InsuranceClaims.Data.DbModels.CompanySchema;
using InsuranceClaims.Data.DbModels.CustomerSchema;
using InsuranceClaims.Data.DbModels.LookupSchema;
using InsuranceClaims.Data.DbModels.PaymentSchema;
using InsuranceClaims.Data.DbModels.PolicySchema;
using InsuranceClaims.Data.DbModels.SecuritySchema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InsuranceClaims.Data.DataContext
{
    public class AppDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, int,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // set application user relations
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                //b.Navigation(e => e.Company).IsRequired();
            });

            // set application role relations
            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // set application role primary key
                b.HasKey(u => u.Id);
                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            // set application user role primary key
            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.HasKey(u => u.Id);
            });

            // Update Identity Schema
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "Security");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles", "Security");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles", "Security");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins", "Security");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims", "Security");
            modelBuilder.Entity<ApplicationUserToken>().ToTable("UserTokens", "Security");
            modelBuilder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims", "Security");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            // customer configurations
            modelBuilder.Entity<Customer>(c =>
            {
                c.Property(c => c.Code).IsRequired();
                c.Property(c => c.IsBusiness).IsRequired();
                c.Property(c => c.TamisNumber).IsRequired();
                c.Navigation(c => c.Company).IsRequired();
                c.Navigation(c => c.AccountManager).IsRequired(); // needs more investigation
                c.HasMany(c => c.CustomerContacts);
                c.HasMany(c => c.CustomerBeneficiaries);
                c.HasMany(c => c.CustomerDependents);
                c.HasMany(c => c.Bills);
                c.HasMany(c => c.Payments);
                c.HasMany(c => c.Prepayments);
                c.HasMany(c => c.Claims);
                c.HasMany(c => c.Policies);
            });

            modelBuilder.Entity<CustomerBeneficiary>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.Property(c => c.Allocation).IsRequired();
                c.Navigation(c => c.Customer).IsRequired();
                c.HasMany(c => c.Policies);
            });

            modelBuilder.Entity<CustomerContact>(c =>
            {
                c.Property(c => c.Address1).IsRequired();
                c.Property(c => c.PrimaryEmail).IsRequired();
                c.Property(c => c.CellPhone).IsRequired();
                c.Property(c => c.WorkPhone).IsRequired();
                c.Navigation(c => c.Customer).IsRequired();
                c.Navigation(c => c.Country).IsRequired();
            });

            modelBuilder.Entity<CustomerDependent>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.Property(c => c.DateOfBirth).IsRequired();
                c.Navigation(c => c.Customer).IsRequired();
                c.HasMany(c => c.Policies);
            });

            // company configurations
            modelBuilder.Entity<Company>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.Property(c => c.Email).IsRequired();
                c.Property(c => c.Url).IsRequired();
                c.Property(c => c.BusinessRegisterationNumber).IsRequired();
                c.Property(c => c.TaxRegisterationNumber).IsRequired();
                c.Navigation(c => c.Country).IsRequired();
                c.HasMany(c => c.Customers);
                c.HasMany(c => c.CompanyContacts);
                c.HasMany(c => c.CompanyEmployers);
                c.HasMany(c => c.CompanyPaymentMethods);
                c.HasMany(c => c.CompanyAttachmentTypes);
                c.HasMany(c => c.PolicyTypes);
                c.HasMany(c => c.CompanyMinorAges);
                c.HasMany(c => c.CompanyCollectionMethods);
                c.HasMany(c => c.CompanyDenominations);
            });

            modelBuilder
                        .Entity<Company>()
                        .HasMany(p => p.PolicyTypes)
                        .WithMany(p => p.Companies)
                        .UsingEntity(j => j.ToTable("CompanyPolicyTypes", "Companies"));

            modelBuilder.Entity<CompanyAttachmentType>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.Navigation(c => c.Company).IsRequired();
            });

            modelBuilder.Entity<CompanyCollectionMethod>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.Navigation(c => c.Company).IsRequired();
                c.HasMany(c => c.Claims);
            });

            modelBuilder.Entity<CompanyContact>(c =>
            {
                c.Property(c => c.Reference).IsRequired();
                c.Property(c => c.FirstName).IsRequired();
                c.Property(c => c.LastName).IsRequired();
                c.Property(c => c.BusinessAddress).IsRequired();
                c.Property(c => c.BusinessPhone).IsRequired();
                c.Property(c => c.CellPhone).IsRequired();
                c.Property(c => c.EmailAddress).IsRequired();
                c.Navigation(c => c.Company).IsRequired();
            });

            modelBuilder.Entity<CompanyDenomination>(c =>
            {
                c.Property(c => c.DenominationValue).IsRequired();
                c.Navigation(c => c.Company).IsRequired();
                c.HasMany(c => c.CashDenominations);
                c.HasMany(c => c.CashDenominationReturns);
            });

            modelBuilder.Entity<CompanyEmployer>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.Navigation(c => c.Company).IsRequired();
                c.HasMany(c => c.Customers);
            });

            modelBuilder.Entity<CompanyMinorAge>(c =>
            {
                c.Property(c => c.AgeValue).IsRequired();
                c.Navigation(c => c.Company).IsRequired();
            });

            modelBuilder.Entity<CompanyPaymentMethod>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.Navigation(c => c.Company).IsRequired();
                c.HasMany(c => c.PaymentAllocations);
            });

            // claim configurations
            modelBuilder.Entity<Claim>(c =>
            {
                c.Property(c => c.ReferenceNumber).IsRequired();
                c.Property(c => c.SystemNumber).IsRequired();
                c.Property(c => c.Patient).IsRequired();
                c.Property(c => c.DateReceived).IsRequired();
                c.Property(c => c.DateSentToInsurance).IsRequired();
                c.Property(c => c.DateReturnedFromInsurance).IsRequired();
                c.Property(c => c.DateChequeIssued).IsRequired();
                c.Property(c => c.ChequeNumber).IsRequired();
                c.Property(c => c.ChequeAmount).IsRequired();
                c.Navigation(c => c.ClaimStatus).IsRequired();
                c.Navigation(c => c.Customer).IsRequired();
                c.Navigation(c => c.Policy).IsRequired();
                c.Navigation(c => c.CollectionMethod).IsRequired();
                c.HasMany(c => c.ClaimAttachments);
            });

            modelBuilder.Entity<ClaimAttachment>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.Property(c => c.AttachmentPath).IsRequired();
                c.Property(c => c.Size).IsRequired();
                c.Navigation(c => c.Claim).IsRequired();
            });

            // lookup configurations
            modelBuilder.Entity<ClaimStatus>(c =>
            {
                c.HasMany(c => c.Claims);
            });

            modelBuilder.Entity<Country>(c =>
            {
                c.Property(c => c.Code).IsRequired();
                c.Property(c => c.NativeName).IsRequired();
                c.Property(c => c.Name).IsRequired();
                c.HasMany(c => c.CustomerContacts);
                c.HasMany(c => c.Companies);
            });

            modelBuilder.Entity<CoverageType>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.HasMany(c => c.Policies);
            });

            modelBuilder.Entity<IdentificationType>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.HasMany(c => c.PolicyIssuredDrivers);
            });

            modelBuilder.Entity<PolicyInsurer>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.HasMany(c => c.PolicyTypes);
            });

            modelBuilder.Entity<PolicyType>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.HasMany(c => c.Companies);
                c.HasMany(c => c.Policies);
                c.Navigation(c => c.PolicyInsurer).IsRequired();
            });

            // payment configurations
            modelBuilder.Entity<Bill>(c =>
            {
                c.Property(c => c.ReferenceNumber).IsRequired();
                c.Property(c => c.Amount).IsRequired();
                c.Property(c => c.BillDate).IsRequired();
                c.Property(c => c.PaymentDue).IsRequired();
                c.Navigation(c => c.Policy).IsRequired();
                c.HasMany(c => c.PaymentAllocations);
            });

            modelBuilder.Entity<CashDenomination>(c =>
            {
                c.Property(c => c.Quantity).IsRequired();
                c.Property(c => c.Amount).IsRequired();
                c.Property(c => c.Denomination).IsRequired();
                c.Navigation(c => c.Payment).IsRequired();
                c.Navigation(c => c.CompanyDenomination).IsRequired();
            });

            modelBuilder.Entity<CashDenominationReturn>(c =>
            {
                c.Property(c => c.Quantity).IsRequired();
                c.Property(c => c.Amount).IsRequired();
                c.Property(c => c.Denomination).IsRequired();
                c.Navigation(c => c.Payment).IsRequired();
                c.Navigation(c => c.CompanyDenomination).IsRequired();
            });

            modelBuilder.Entity<Payment>(c =>
            {
                c.Property(c => c.PaymentDate).IsRequired();
                c.Property(c => c.ReceiptNumber).IsRequired();
                c.Property(c => c.PaidBy).IsRequired();
                c.Property(c => c.Amount).IsRequired();
                c.Navigation(c => c.Customer).IsRequired();
                c.HasMany(c => c.PaymentAllocations);
                c.HasMany(c => c.CashDenominations);
                c.HasMany(c => c.CashDenominationReturns);
            });

            modelBuilder.Entity<PaymentAllocation>(c =>
            {
                c.Property(c => c.PaidTo).IsRequired();
                c.Property(c => c.AmountPaid).IsRequired();
                c.Property(c => c.PaymentDue).IsRequired();
                c.Navigation(c => c.Payment).IsRequired();
                c.Navigation(c => c.PaymentMethod).IsRequired();
            });

            modelBuilder.Entity<Prepayment>(c =>
            {
                c.Property(c => c.PaidBy).IsRequired();
                c.Property(c => c.Amount).IsRequired();
                c.Navigation(c => c.Policy).IsRequired();
                c.HasMany(c => c.PaymentAllocations);
            });

            // policy configurations
            modelBuilder.Entity<Policy>(c =>
            {
                c.Property(c => c.Name).IsRequired();
                c.Navigation(c => c.PolicyType).IsRequired();
                c.Navigation(c => c.Customer).IsRequired();
                c.HasMany(c => c.CustomerBeneficiaries);
                c.HasMany(c => c.CustomerDependents);
                c.HasMany(c => c.PolicyInsuredDrivers);
                c.HasMany(c => c.PolicyContents);
                c.HasMany(c => c.Bills);
                c.HasMany(c => c.PaymentAllocations);
                c.HasMany(c => c.Prepayments);
                c.HasMany(c => c.Claims);
            });

            modelBuilder
                      .Entity<Policy>()
                      .HasMany(p => p.CustomerBeneficiaries)
                      .WithMany(p => p.Policies)
                      .UsingEntity(j => j.ToTable("CustomerBeneficiaryPolicies", "Policies"));

            modelBuilder
               .Entity<Policy>()
               .HasMany(p => p.CustomerDependents)
               .WithMany(p => p.Policies)
               .UsingEntity(j => j.ToTable("CustomerDependentPolicies", "Policies"));

            modelBuilder.Entity<PolicyContent>(c =>
            {
                c.Property(c => c.Content).IsRequired();
                c.Property(c => c.Value).IsRequired();
                c.Navigation(c => c.Policy).IsRequired();
            });

            modelBuilder.Entity<PolicyInsuredDriver>(c =>
            {
                c.Property(c => c.Type).IsRequired();
                c.Property(c => c.FirstName).IsRequired();
                c.Property(c => c.LastName).IsRequired();
                c.Property(c => c.DateOfBirth).IsRequired();
                c.Property(c => c.Reference).IsRequired();
                c.Navigation(c => c.Policy).IsRequired();
                c.Navigation(c => c.IdentificationType).IsRequired();
            });
        }

        #region Company Schema
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyAttachmentType> CompanyAttachmentTypes { get; set; }
        public DbSet<CompanyCollectionMethod> CompanyCollectionMethods { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<CompanyDenomination> CompanyDenominations { get; set; }
        public DbSet<CompanyEmployer> CompanyEmployers { get; set; }
        public DbSet<CompanyMinorAge> CompanyMinorAges { get; set; }
        public DbSet<CompanyPaymentMethod> CompanyPaymentMethods { get; set; }
        #endregion

        #region Claim Schema
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimAttachment> ClaimAttachments { get; set; }
        #endregion

        #region Customer Schema
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<CustomerBeneficiary> CustomerBeneficiaries { get; set; }
        public DbSet<CustomerDependent> CustomerDependents { get; set; }
        #endregion

        #region Payment Schema
        public DbSet<Bill> Bills { get; set; }
        public DbSet<CashDenomination> CashDenominations { get; set; }
        public DbSet<CashDenominationReturn> CashDenominationReturns { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentAllocation> PaymentAllocations { get; set; }
        public DbSet<Prepayment> Prepayments { get; set; }
        #endregion

        #region Policy Schema
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyContent> PolicyContents { get; set; }
        public DbSet<PolicyInsuredDriver> PolicyInsuredDrivers { get; set; }
        #endregion

        #region Lookup schema
        public DbSet<ClaimStatus> ClaimStatuses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CoverageType> CoverageTypes { get; set; }
        public DbSet<IdentificationType> IdentificationTypes { get; set; }
        public DbSet<PolicyInsurer> PolicyInsurers { get; set; }
        public DbSet<PolicyType> PolicyTypes { get; set; }
        #endregion

        #region Audits 
        #endregion
    }
}

using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.ClaimSchema;
using InsuranceClaims.Data.DbModels.CompanySchema;
using InsuranceClaims.Data.DbModels.PaymentSchema;
using InsuranceClaims.Data.DbModels.PolicySchema;
using InsuranceClaims.Data.DbModels.SecuritySchema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CustomerSchema
{
    [Table("Customers", Schema = "Customers")]
    public class Customer : BaseEntity
    {
        public Customer()
        {
            CustomerContacts = new HashSet<CustomerContact>();
            CustomerBeneficiaries = new HashSet<CustomerBeneficiary>();
            CustomerDependents = new HashSet<CustomerDependent>();
            Policies = new HashSet<Policy>();
            Claims = new HashSet<Claim>();
            Bills = new HashSet<Bill>();
            Payments = new HashSet<Payment>();
            Prepayments = new HashSet<Prepayment>();
        }

        // Business and Individual
        public string Code { get; set; }
        public bool IsBusiness { get; set; }
        public string TamisNumber { get; set; }
        public string NisNumber { get; set; }

        // Business Customer
        public string RegisterationNumber { get; set; }
        public string BusinessName { get; set; }

        // Individual Customer
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string NationalId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Minor { get; set; } // if yes then we should validate date of birth against minor age from company configuration
        public string ParentGuardian { get; set; } // if minor is yes
        public string JobTitle { get; set; }
        public DateTime? DateEmployed { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyEmployer Employer { get; set; }
        public virtual ApplicationUser AccountManager { get; set; }
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        public virtual ICollection<CustomerBeneficiary> CustomerBeneficiaries { get; set; }
        public virtual ICollection<CustomerDependent> CustomerDependents { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Prepayment> Prepayments { get; set; }
    }
}

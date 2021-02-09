using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CustomerSchema;
using InsuranceClaims.Data.DbModels.LookupSchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CompanySchema
{
    [Table("Companies", Schema = "Companies")]
    public class Company : BaseEntity
    {
        public Company()
        {
            Customers = new HashSet<Customer>();
            CompanyContacts = new HashSet<CompanyContact>();
            CompanyEmployers = new HashSet<CompanyEmployer>();
            CompanyPaymentMethods = new HashSet<CompanyPaymentMethod>();
            CompanyAttachmentTypes = new HashSet<CompanyAttachmentType>();
            PolicyTypes = new HashSet<PolicyType>();
            CompanyMinorAges = new HashSet<CompanyMinorAge>();
            CompanyCollectionMethods = new HashSet<CompanyCollectionMethod>();
            CompanyDenominations = new HashSet<CompanyDenomination>();
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }

        // User Managment
        public int NumOfDaysToChangePassword { get; set; }
        public int AccountLoginAttempts { get; set; }
        public int PasswordExpiryTime { get; set; }
        public double UserPhotosize { get; set; }
        public double AttachmentsMaxSize { get; set; }
        public int TimesCountBeforePasswordReuse { get; set; }
        public int TimeToSessionTimeOut { get; set; }

        // Company 
        public string BusinessRegisterationNumber { get; set; }
        public string TaxRegisterationNumber { get; set; }
        public string LogoPath { get; set; }
        public long BeginingReceiptNumber { get; set; } // after register through config section 
        public bool AutoPaymentOnAddingNewPrepayment { get; set; }
        public bool ShowCashAndChangePage { get; set; } // this property controls the visibility of cash and change tabs

        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
        public virtual ICollection<CompanyEmployer> CompanyEmployers { get; set; }
        public virtual ICollection<CompanyPaymentMethod> CompanyPaymentMethods { get; set; }
        public virtual ICollection<CompanyAttachmentType> CompanyAttachmentTypes { get; set; }
        public virtual ICollection<PolicyType> PolicyTypes { get; set; }
        public virtual ICollection<CompanyMinorAge> CompanyMinorAges { get; set; }
        public virtual ICollection<CompanyCollectionMethod> CompanyCollectionMethods { get; set; }
        public virtual ICollection<CompanyDenomination> CompanyDenominations { get; set; }
    }
}

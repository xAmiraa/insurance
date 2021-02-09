using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.ClaimSchema;
using InsuranceClaims.Data.DbModels.CustomerSchema;
using InsuranceClaims.Data.DbModels.LookupSchema;
using InsuranceClaims.Data.DbModels.PaymentSchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.PolicySchema
{
    [Table("Policies", Schema = "Policies")]
    public class Policy : BaseEntity
    {
        public Policy()
        {
            CustomerBeneficiaries = new HashSet<CustomerBeneficiary>();
            PolicyInsuredDrivers = new HashSet<PolicyInsuredDriver>();
            CustomerDependents = new HashSet<CustomerDependent>();
            PolicyContents = new HashSet<PolicyContent>();
            Bills = new HashSet<Bill>();
            PaymentAllocations = new HashSet<PaymentAllocation>();
            Prepayments = new HashSet<Prepayment>();
            Claims = new HashSet<Claim>();
        }
        public string Name { get; set; }
        public string Description { get; set; }

        // Properties based on policy type
        public double? SumInsured { get; set; } // group life, healthcare heroes, frontline heroes, individual life, term insurance, peace assured
        public double? CriticalIllness { get; set; } // healthcare heroes, frontline heroes
        public double? DependentCover { get; set; } // healthcare heroes, frontline heroes
        public double? LevelOfBenefit { get; set; } // healthcare heroes, frontline heroes

        public int? AgeOfMaturity { get; set; } // pensions
        public bool? IsRegistered { get; set; } // pensions
        public decimal? DepositAmount { get; set; } // pensions
        public decimal? InstallmentAmount { get; set; } // pensions
        public int? LengthOfTerm { get; set; } // term insurance, peace assured

        public string EngineNumber { get; set; } // private motor, commercial motor
        public string LicenseNumber { get; set; } // private motor, commercial motor
        public string ChasisNumber { get; set; } // private motor, commercial motor
        public string Model { get; set; } // private motor, commercial motor
        public string Make { get; set; } // private motor, commercial motor
        public string Color { get; set; } // private motor, commercial motor
        public int? SeatCapacity { get; set; } // private motor, commercial motor
        public string EngineCC { get; set; } // private motor, commercial motor

        public string CompanyName { get; set; }// commercial motor, commercial fire

        public string Location { get; set; } // home owners, public liability, special event public liability, commercial fire
        public string Value { get; set; } // home owners, commercial fire
        public string Construction { get; set; }// home owners
        public string Foundation { get; set; } // home owners
        public string Contents { get; set; } // home owners
        public bool? Mortagagee { get; set; } // home owners
        public string WhoMortagagee { get; set; } // home owners

        public decimal? Amount { get; set; } // public liability, special event public liability, travel
        public int? MaximumPerOccurence { get; set; }// public liability, special event public liability
        public int? Capacity { get; set; }// public liability, special event public liability

        public string MachineryType { get; set; } // machinery
        public string MachineryValue { get; set; } // machinery
        public bool? MachineryOwned { get; set; } // machinery
        public string WhoMachinery { get; set; } // machinery

        public string Itinerary { get; set; } // travel
        public string ModeOfTravel { get; set; } // travel

        public bool? CommercialOwned { get; set; } // commercial fire
        public string WhoCommercial { get; set; } // commercial fire

        public virtual PolicyType PolicyType { get; set; }
        public virtual CoverageType CoverageType { get; set; } // group medical
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CustomerBeneficiary> CustomerBeneficiaries { get; set; } // group medical, group life, healthcare heroes, frontline heroes, individual life, term insurance, peace assured
        public virtual ICollection<CustomerDependent> CustomerDependents { get; set; } // group medical
        public virtual ICollection<PolicyInsuredDriver> PolicyInsuredDrivers { get; set; } // private motor, commercial motor
        public virtual ICollection<PolicyContent> PolicyContents { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<PaymentAllocation> PaymentAllocations { get; set; }
        public virtual ICollection<Prepayment> Prepayments { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
    }
}

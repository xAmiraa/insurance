using InsuranceClaims.Core.Common;
using System;

namespace InsuranceClaims.DTO.Customer.Customer
{
    public class CustomerDto : BaseEntityDto
    {
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

        public LookupDto Employer { get; set; }
        public LookupDto AccountManager { get; set; }
    }
}

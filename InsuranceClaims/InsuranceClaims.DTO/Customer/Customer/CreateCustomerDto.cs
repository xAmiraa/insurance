using InsuranceClaims.DTO.Company.Employer;
using InsuranceClaims.DTO.Customer.Beneficiary;
using InsuranceClaims.DTO.Customer.CustomerContact;
using System;
using System.Collections.Generic;

namespace InsuranceClaims.DTO.Customer.Customer
{
    public class CreateCustomerDto
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

        public int AccountManagerId { get; set; }
        public CreateUpdateEmployerDto Employer { get; set; }
        public List<CreateUpdateCustomerContactDto> CustomerContacts { get; set; }
        public List<CreateUpdateBeneficiaryDto> CustomerBeneficiaries { get; set; }
    }
}

using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Company.Contact
{
    public class CompanyContactDto : BaseEntityDto
    {
        public string Reference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPhone { get; set; }
        public string CellPhone { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }

        public LookupDto BusinessCountry { get; set; }
    }
}

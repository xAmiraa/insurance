using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Company.Contact
{
    public class CompanyContactFilterDto : BaseFilterDto
    {
        public string Reference { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }
    }
}

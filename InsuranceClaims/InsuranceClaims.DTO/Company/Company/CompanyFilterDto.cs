using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Company.Company
{
    public class CompanyFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string TaxRegisterationNumber { get; set; }
        public string BusinessRegisterationNumber { get; set; }
        public int? CountryId { get; set; }
    }
}

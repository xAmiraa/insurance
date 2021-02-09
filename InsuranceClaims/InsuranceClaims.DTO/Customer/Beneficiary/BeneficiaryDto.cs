using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Customer.Beneficiary
{
    public class BeneficiaryDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Allocation { get; set; }
    }
}

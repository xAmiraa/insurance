using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Lookup.PolicyInsurer
{
    public class PolicyInsurerDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

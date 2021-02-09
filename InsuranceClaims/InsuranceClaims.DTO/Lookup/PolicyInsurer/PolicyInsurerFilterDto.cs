using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Lookup.PolicyInsurer
{
    public class PolicyInsurerFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}

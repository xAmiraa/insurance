using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Lookup.IdentificationType
{
    public class IdentificationTypeFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}

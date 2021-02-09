using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Company.AttachmentType
{
    public class AttachmentTypeFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}

using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Company.MinorAge
{
    public class MinorAgeFilterDto : BaseFilterDto
    {
        public int? AgeValue { get; set; }
    }
}

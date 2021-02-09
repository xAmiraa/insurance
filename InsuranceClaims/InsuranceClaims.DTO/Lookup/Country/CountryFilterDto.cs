using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Lookup.Country
{
    public class CountryFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string NativeName { get; set; }
        public bool? IsActive { get; set; }
    }
}

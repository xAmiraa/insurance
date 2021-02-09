using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Lookup.Currency
{
    public class CurrencyFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
    }
}

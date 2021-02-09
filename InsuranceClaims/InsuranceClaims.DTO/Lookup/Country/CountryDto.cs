using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Lookup.Country
{
    public class CountryDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Flag { get; set; }
        public string NativeName { get; set; }
        public string CurrencyCode { get; set; }
        public string CallingCode { get; set; }
    }
}

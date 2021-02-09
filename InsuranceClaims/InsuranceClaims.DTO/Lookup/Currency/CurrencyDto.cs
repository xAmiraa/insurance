using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Lookup.Currency
{
    public class CurrencyDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Customer.Customer
{
    public class CustomerFilterDto : BaseFilterDto
    {
        public bool? IsBusiness { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string TamisNumber { get; set; }
        public string NisNumber { get; set; }
    }
}

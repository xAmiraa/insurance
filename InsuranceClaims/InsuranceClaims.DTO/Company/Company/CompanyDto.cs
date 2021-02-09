using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Company.Company
{
    public class CompanyDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }

        // Company 
        public string BusinessRegisterationNumber { get; set; }
        public string TaxRegisterationNumber { get; set; }
        public string LogoPath { get; set; }
        public long BeginingReceiptNumber { get; set; } // after register through config section 

        public LookupDto Country { get; set; }
        public LookupDto Currency { get; set; }
    }
}

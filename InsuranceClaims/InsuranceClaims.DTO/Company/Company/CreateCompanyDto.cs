namespace InsuranceClaims.DTO.Company.Company
{
    public class CreateCompanyDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
    }
}

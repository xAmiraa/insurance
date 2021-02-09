namespace InsuranceClaims.DTO.Company.Contact
{
    public class CreateUpdateCompanyContactDto
    {
        public string Reference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPhone { get; set; }
        public string CellPhone { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }

        public int BusinessCountryId { get; set; }
    }
}

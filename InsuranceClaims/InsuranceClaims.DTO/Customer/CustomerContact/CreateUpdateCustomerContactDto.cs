namespace InsuranceClaims.DTO.Customer.CustomerContact
{
    public class CreateUpdateCustomerContactDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Parish { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }

        public int CountryId { get; set; }
    }
}

namespace InsuranceClaims.DTO.Security.User
{
    public class CreateUpdateUserDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string GroupName { get; set; } // GroupsEnum
        public int RoleId { get; set; }
    }
}

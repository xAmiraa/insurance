using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Security.User
{
    public class UserDto : BaseEntityDto
    {
        public string PersonalImagePath { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; } // Active, NotActive, Locked
        public string GroupName { get; set; }
        public LookupDto Role { get; set; }
    }
}

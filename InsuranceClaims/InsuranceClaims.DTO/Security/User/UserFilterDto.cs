using InsuranceClaims.Core.Common;

namespace InsuranceClaims.DTO.Security.User
{
    public class UserFilterDto : BaseFilterDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Status { get; set; } // Active, NotActive, Locked
        public string GroupName { get; set; } // GroupsEnum
    }
}

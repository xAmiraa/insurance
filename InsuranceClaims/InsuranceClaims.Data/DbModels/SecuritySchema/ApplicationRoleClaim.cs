using Microsoft.AspNetCore.Identity;

namespace InsuranceClaims.Data.DbModels.SecuritySchema
{
    public class ApplicationRoleClaim : IdentityRoleClaim<int>
    {
        public virtual ApplicationRole Role { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace InsuranceClaims.Data.DbModels.SecuritySchema
{
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}

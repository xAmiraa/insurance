using Microsoft.AspNetCore.Identity;

namespace InsuranceClaims.Data.DbModels.SecuritySchema
{
    public class ApplicationUserToken : IdentityUserToken<int>
    {
        public virtual ApplicationUser User { get; set; }       
    }
}

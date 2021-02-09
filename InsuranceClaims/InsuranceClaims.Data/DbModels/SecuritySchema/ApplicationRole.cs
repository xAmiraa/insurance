using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.SecuritySchema
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {
            RoleClaims = new HashSet<ApplicationRoleClaim>();
           // UserRoles = new HashSet<ApplicationUserRole>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)] // so you can insert role id from enum while seeding
        public override int Id { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
        //public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}

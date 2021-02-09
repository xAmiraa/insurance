using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.ClaimSchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.LookupSchema
{
    /// <summary>
    /// It describe the status of claim. It should be Pending , Pending with query, Cancelled , Completed
    /// </summary>
    [Table("ClaimStatuses", Schema = "Lookups")]
    public class ClaimStatus : StaticLookup
    {
        public ClaimStatus()
        {
            Claims = new HashSet<Claim>();
        }

        public virtual ICollection<Claim> Claims { get; set; }
    }
}

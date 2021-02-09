using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.ClaimSchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CompanySchema
{
    [Table("CompanyCollectionMethods", Schema = "Companies")]
    public class CompanyCollectionMethod : BaseEntity
    {
        public CompanyCollectionMethod()
        {
            Claims = new HashSet<Claim>();
        }

        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
    }
}

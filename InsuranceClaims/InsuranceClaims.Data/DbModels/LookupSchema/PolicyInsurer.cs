using InsuranceClaims.Data.BaseModeling;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.LookupSchema
{
    [Table("PolicyInsurers", Schema = "Lookups")]
    public class PolicyInsurer : BaseEntity
    {
        public PolicyInsurer()
        {
            PolicyTypes = new HashSet<PolicyType>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PolicyType> PolicyTypes { get; set; }
    }
}

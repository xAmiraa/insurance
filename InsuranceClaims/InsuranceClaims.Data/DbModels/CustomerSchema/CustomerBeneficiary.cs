using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.PolicySchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CustomerSchema
{
    [Table("Beneficiaries", Schema = "Customers")]
    public class CustomerBeneficiary : BaseEntity
    {
        public CustomerBeneficiary()
        {
            Policies = new HashSet<Policy>();
        }

        public string Name { get; set; }
        public string Allocation { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }
    }
}

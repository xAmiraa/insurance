using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.PolicySchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.LookupSchema
{
    [Table("IdentificationTypes", Schema = "Lookups")]
    public class IdentificationType : BaseEntity
    {
        public IdentificationType()
        {
            PolicyIssuredDrivers = new HashSet<PolicyInsuredDriver>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PolicyInsuredDriver> PolicyIssuredDrivers { get; set; }
    }
}

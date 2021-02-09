using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CompanySchema;
using InsuranceClaims.Data.DbModels.PolicySchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.LookupSchema
{
    [Table("PolicyTypes", Schema = "Lookups")]
    public class PolicyType : StaticLookup
    {
        public PolicyType()
        {
            Companies = new HashSet<Company>();
            Policies = new HashSet<Policy>();
        }

        public virtual PolicyInsurer PolicyInsurer { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }
    }
}

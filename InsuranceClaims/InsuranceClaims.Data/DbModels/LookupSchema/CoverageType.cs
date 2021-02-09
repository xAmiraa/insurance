using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.PolicySchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.LookupSchema
{
    /// <summary>
    /// It describe coverage type. It should be Employee only, Employee and Child, Employee and Souse, Employee and Family
    /// </summary>
    [Table("CoverageTypes", Schema = "Lookups")]
    public class CoverageType : StaticLookup
    {
        public CoverageType()
        {
            Policies = new HashSet<Policy>();
        }

        public virtual ICollection<Policy> Policies { get; set; }
    }
}

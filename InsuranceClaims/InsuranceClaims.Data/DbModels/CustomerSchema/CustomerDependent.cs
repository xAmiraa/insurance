using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.PolicySchema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CustomerSchema
{
    [Table("Dependents", Schema = "Customers")]
    public class CustomerDependent : BaseEntity
    {
        public CustomerDependent()
        {
            Policies = new HashSet<Policy>();
        }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }
    }
}

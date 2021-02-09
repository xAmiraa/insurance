using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.LookupSchema;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace InsuranceClaims.Data.DbModels.PolicySchema
{
    [Table("PolicyInsuredDrivers", Schema = "Policies")]
    public class PolicyInsuredDriver : BaseEntity
    {
        public string Type { get; set; } // Primary, Secondary
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Reference { get; set; }

        public virtual Policy Policy { get; set; }
        public virtual IdentificationType IdentificationType { get; set; }
    }
}

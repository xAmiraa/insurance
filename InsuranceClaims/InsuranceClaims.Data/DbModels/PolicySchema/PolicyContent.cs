using InsuranceClaims.Data.BaseModeling;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.PolicySchema
{
    [Table("PolicyContents", Schema = "Policies")]
    public class PolicyContent : BaseEntity
    {
        public string Content { get; set; }
        public string Value { get; set; }

        public virtual Policy Policy { get; set; }
    }
}

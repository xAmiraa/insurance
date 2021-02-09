using InsuranceClaims.Data.BaseModeling;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.ClaimSchema
{
    [Table("ClaimAttachments", Schema = "Claims")]
    public class ClaimAttachment : BaseEntity
    {
        public string Name { get; set; }
        public string AttachmentPath { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public virtual Claim Claim { get; set; }
    }
}

using InsuranceClaims.Data.BaseModeling;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CompanySchema
{
    [Table("CompanyAttachmentTypes", Schema = "Companies")]
    public class CompanyAttachmentType : BaseEntity
    {
        public string Name { get; set; }

        public virtual Company Company { get; set; }
    }
}

using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.Data.DbModels.SecuritySchema;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.BaseModeling
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Creator")]
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("Updator")]
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ApplicationUser Creator { get; set; }
        public virtual ApplicationUser Updator { get; set; }
    }
}

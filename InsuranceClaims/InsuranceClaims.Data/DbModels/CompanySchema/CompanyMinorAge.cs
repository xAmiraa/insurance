using InsuranceClaims.Data.BaseModeling;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CompanySchema
{
    [Table("CompanyMinorAges", Schema = "Companies")]
    public class CompanyMinorAge : BaseEntity
    {
        public int AgeValue { get; set; }
        public virtual Company Company { get; set; }
    }
}

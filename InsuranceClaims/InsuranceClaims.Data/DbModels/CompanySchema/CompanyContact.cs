using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.LookupSchema;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CompanySchema
{
    [Table("CompanyContacts", Schema = "Companies")]
    public class CompanyContact : BaseEntity
    {
        public string Reference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPhone { get; set; }
        public string CellPhone { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }

        public virtual Company Company { get; set; }
        public virtual Country BusinessCountry { get; set; }
    }
}

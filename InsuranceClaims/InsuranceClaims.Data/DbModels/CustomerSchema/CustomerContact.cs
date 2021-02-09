using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.LookupSchema;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CustomerSchema
{
    [Table("CustomerContacts", Schema = "Customers")]
    public class CustomerContact : BaseEntity
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Parish { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Country Country { get; set; }
    }
}

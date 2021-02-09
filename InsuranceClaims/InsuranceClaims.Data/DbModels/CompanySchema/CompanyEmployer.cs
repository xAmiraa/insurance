using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CustomerSchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CompanySchema
{
    [Table("CompanyEmployers", Schema = "Companies")]
    public class CompanyEmployer : BaseEntity
    {
        public CompanyEmployer()
        {
            Customers = new HashSet<Customer>();
        }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}

using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CompanySchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.LookupSchema
{
    [Table("Currencies", Schema = "Lookups")]
    public  class Currency : BaseEntity
    {
        public Currency()
        {
            Companies = new HashSet<Company>();
        }
        public string Name { get; set; }
        public string Code { get; set; }


        public virtual ICollection<Company> Companies { get; set; }
    }
}

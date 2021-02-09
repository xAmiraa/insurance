using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CompanySchema;
using InsuranceClaims.Data.DbModels.CustomerSchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.LookupSchema
{
    [Table("Countries", Schema = "Lookups")]
    public  class Country : BaseEntity
    {
        public Country()
        {
            CustomerContacts = new HashSet<CustomerContact>();
            Companies = new HashSet<Company>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Flag { get; set; }
        public string NativeName { get; set; }
        public string CurrencyCode { get; set; }
        public string CallingCode { get; set; }

        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}

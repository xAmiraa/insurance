using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.PaymentSchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CompanySchema
{
    [Table("CompanyPaymentMethods", Schema = "Companies")]
    public class CompanyPaymentMethod : BaseEntity
    {
        public CompanyPaymentMethod()
        {
            PaymentAllocations = new HashSet<PaymentAllocation>();
        }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<PaymentAllocation> PaymentAllocations { get; set; }
    }
}

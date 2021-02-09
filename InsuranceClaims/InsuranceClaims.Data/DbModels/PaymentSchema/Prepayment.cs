using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CustomerSchema;
using InsuranceClaims.Data.DbModels.PolicySchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.PaymentSchema
{
    [Table("Prepayments", Schema = "Payments")]
    public class Prepayment : BaseEntity
    {
        public Prepayment()
        {
            PaymentAllocations = new HashSet<PaymentAllocation>();
        }

        public decimal Amount { get; set; } // part of the amount may be reassigned . so we need to handle that and add log
        public string PaidBy { get; set; }


        public virtual Policy Policy { get; set; }
        public virtual ICollection<PaymentAllocation> PaymentAllocations { get; set; }
    }
}

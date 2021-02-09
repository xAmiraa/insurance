using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.PolicySchema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.PaymentSchema
{
    [Table("Bills", Schema = "Payments")]
    public class Bill : BaseEntity
    {
        public Bill()
        {
            PaymentAllocations = new HashSet<PaymentAllocation>();
        }
        public string ReferenceNumber { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaymentDue { get; set; }

        public virtual Policy Policy { get; set; }
        public virtual ICollection<PaymentAllocation> PaymentAllocations { get; set; }
    }
}

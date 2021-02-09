using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CompanySchema;
using InsuranceClaims.Data.DbModels.PolicySchema;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.PaymentSchema
{
    [Table("PaymentAllocations", Schema = "Payments")]
    public class PaymentAllocation : BaseEntity
    {
        public string PaidTo { get; set; } // bill, union fees, prepayment
        public string Details { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal PaymentDue { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Policy Policy { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual Prepayment Prepayment { get; set; }
        public virtual CompanyPaymentMethod PaymentMethod { get; set; } // if at least one of the selected payment method is cashe then display cash denomination tab
    }
}

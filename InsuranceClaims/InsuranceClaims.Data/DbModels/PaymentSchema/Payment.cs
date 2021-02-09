using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CustomerSchema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.PaymentSchema
{
    [Table("Payments", Schema = "Payments")]
    public class Payment : BaseEntity
    {
        public Payment()
        {
            PaymentAllocations = new HashSet<PaymentAllocation>();
            CashDenominations = new HashSet<CashDenomination>();
            CashDenominationReturns = new HashSet<CashDenominationReturn>();
        }

        public DateTime PaymentDate { get; set; } // todays date
        public string ReceiptNumber { get; set; } // based on config section from company to be incremental
        public string PaidBy { get; set; } // free text
        public decimal Amount { get; set; } // total amount of the payment

        public virtual Customer Customer { get; set; }
        public virtual ICollection<PaymentAllocation> PaymentAllocations { get; set; } // its a details of items under the payment
        public virtual ICollection<CashDenomination> CashDenominations { get; set; } // if any cash payment method selected
        public virtual ICollection<CashDenominationReturn> CashDenominationReturns { get; set; } // if cash denomination received more than cash paid
    }
}

using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.PaymentSchema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.CompanySchema
{
    /// <summary>
    /// It should be available if the company support denomination feaature. Range [0.01, 0.05, 0.10, 0.25, 1.00, 5.00, 10.00, 20.00, 50.00, 100.00]
    /// </summary>
    [Table("CompanyDenominations", Schema = "Companies")]
    public class CompanyDenomination : BaseEntity
    {
        public CompanyDenomination()
        {
            CashDenominations = new HashSet<CashDenomination>();
            CashDenominationReturns = new HashSet<CashDenominationReturn>();
        }
        public decimal DenominationValue { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<CashDenomination> CashDenominations { get; set; }
        public virtual ICollection<CashDenominationReturn> CashDenominationReturns { get; set; }
    }
}

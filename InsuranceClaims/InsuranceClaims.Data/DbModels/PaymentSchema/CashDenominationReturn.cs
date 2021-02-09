using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CompanySchema;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.PaymentSchema
{
    /// <summary>
    /// If cash denomination received is more than total paid cash. It should be available if the customer company is supporting denomination feature.
    /// </summary>
    [Table("CashDenominationReturns", Schema = "Payments")]
    public class CashDenominationReturn : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Denomination { get; set; }
        public decimal Amount { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual CompanyDenomination CompanyDenomination { get; set; }
    }
}

using InsuranceClaims.Data.BaseModeling;
using InsuranceClaims.Data.DbModels.CompanySchema;
using InsuranceClaims.Data.DbModels.CustomerSchema;
using InsuranceClaims.Data.DbModels.LookupSchema;
using InsuranceClaims.Data.DbModels.PolicySchema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.DbModels.ClaimSchema
{
    [Table("Claims", Schema = "Claims")]
    public class Claim : BaseEntity
    {
        public Claim()
        {
            ClaimAttachments = new HashSet<ClaimAttachment>();
        }

        public string ReferenceNumber { get; set; }
        public string SystemNumber { get; set; }
        public string Patient { get; set; } // may be name of customer him self or one of dependencies
        public DateTime DateReceived { get; set; }
        public DateTime DateSentToInsurance { get; set; }
        public DateTime DateReturnedFromInsurance { get; set; }
        public DateTime DateChequeIssued { get; set; }
        public string ChequeNumber { get; set; }
        public decimal ChequeAmount { get; set; }
        public string Comments { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CompanyCollectionMethod CollectionMethod { get; set; }
        public virtual ClaimStatus ClaimStatus { get; set; }// by default is pending
        public virtual Policy Policy { get; set; }
        public virtual ICollection<ClaimAttachment> ClaimAttachments { get; set; }
    }
}

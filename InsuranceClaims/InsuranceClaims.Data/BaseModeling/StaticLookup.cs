using InsuranceClaims.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaims.Data.BaseModeling
{
    public class StaticLookup : ILookupEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}


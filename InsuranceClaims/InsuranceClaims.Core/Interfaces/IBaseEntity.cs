using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceClaims.Core.Interfaces
{
    public interface IBaseEntity
    {
        bool IsDeleted { get; set; }
        int Id { get; set; }
    }
}

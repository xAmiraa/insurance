using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Lookup.IdentificationType;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Lookup.IdentificationType
{
    public interface IIdentificationTypeService
    {
        IResponseDTO SearchIdentificationTypes(IdentificationTypeFilterDto filterDto);
        IResponseDTO GetIdentificationTypesDropdown();
        Task<IResponseDTO> CreateIdentificationType(CreateUpdateIdentificationTypeDto options, int userId);
        Task<IResponseDTO> UpdateIdentificationType(int id, CreateUpdateIdentificationTypeDto options, int userId);
        Task<IResponseDTO> RemoveIdentificationType(int id);
        Task<IResponseDTO> UpdateIsActive(int id, bool isActive, int userId);

        // Validate
        IResponseDTO ValidateIdentificationType(CreateUpdateIdentificationTypeDto options, int id = 0);
    }
}

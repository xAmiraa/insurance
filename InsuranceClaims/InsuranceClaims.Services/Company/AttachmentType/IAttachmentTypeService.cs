using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Company.AttachmentType;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Company.AttachmentType
{
    public interface IAttachmentTypeService
    {
        IResponseDTO SearchAttachmentTypes(AttachmentTypeFilterDto filterDto, int companyId);
        IResponseDTO GetAttachmentTypesDropdown(int companyId);
        Task<IResponseDTO> CreateAttachmentType(CreateUpdateAttachmentTypeDto options, int userId, int companyId);
        Task<IResponseDTO> UpdateAttachmentType(int id, CreateUpdateAttachmentTypeDto options, int userId, int companyId);
        Task<IResponseDTO> RemoveAttachmentType(int id, int userId);
        Task<IResponseDTO> UpdateIsActive(int id, bool isActive, int userId);
    }
}

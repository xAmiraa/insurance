using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Lookup.PolicyInsurer;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Lookup.PolicyInsurer
{
    public interface IPolicyInsurerService
    {
        IResponseDTO SearchPolicyInsurers(PolicyInsurerFilterDto filterDto);
        IResponseDTO GetPolicyInsurersDropdown();
        Task<IResponseDTO> CreatePolicyInsurer(CreateUpdatePolicyInsurerDto options, int userId);
        Task<IResponseDTO> UpdatePolicyInsurer(int id, CreateUpdatePolicyInsurerDto options, int userId);
        Task<IResponseDTO> RemovePolicyInsurer(int id);
        Task<IResponseDTO> UpdateIsActive(int id, bool isActive, int userId);

        // Validate
        IResponseDTO ValidatePolicyInsurer(CreateUpdatePolicyInsurerDto options, int id = 0);
    }
}

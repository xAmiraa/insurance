using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Company.Company;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Company.Company
{
    public interface ICompanyService
    {
        IResponseDTO SearchCompanies(CompanyFilterDto filterDto);
        Task<IResponseDTO> CreateCompany(CreateCompanyDto options, int userId);
        Task<IResponseDTO> RemoveCompany(int id, int userId);
    }
}

using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Customer.Customer;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Customer.Customer
{
    public interface ICustomerService
    {
        IResponseDTO SearchCustomers(CustomerFilterDto filterDto, int companyId);
        Task<IResponseDTO> CreateCustomer(CreateCustomerDto options, int userId, int companyId);
        Task<IResponseDTO> RemoveCustomer(int id, int userId);
    }
}

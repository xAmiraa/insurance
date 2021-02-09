using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Security.User;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Security.Account
{
    public interface IAccountService
    {
        Task<IResponseDTO> Login(LoginParamsDto loginParams);
        Task<IResponseDTO> ResetPassword(ResetPasswordParamsDto options);
        Task<IResponseDTO> ChangePassword(int userId, ChangePasswordParamsDto options);
        Task<IResponseDTO> UpdateUserProfile(int id, UpdateUserProfile options, IFormFile file);

        // Users
        IResponseDTO SearchUsers(UserFilterDto filterDto);
        IResponseDTO GetUser(string rootPath, int id);
        Task<IResponseDTO> CreateUser(CreateUpdateUserDto options, int userId);
        Task<IResponseDTO> UpdateUser(int id, CreateUpdateUserDto options, int userId);
        Task<IResponseDTO> DeactivateUser(int id, int userId);
        Task<IResponseDTO> ActivateUser(int id, int userId);
    }
}

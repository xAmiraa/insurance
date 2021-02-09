using InsuranceClaims.API.Helpers;
using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Security.User;
using InsuranceClaims.Services.Security.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsuranceClaims.API.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(
           IAccountService accountService,
           IResponseDTO response,
           IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _accountService = accountService;
        }


        [AllowAnonymous]
        [Route("api/login")]
        [HttpPost]
        public async Task<IResponseDTO> Login([FromBody] LoginParamsDto loginParams)
        {
            _response = await _accountService.Login(loginParams);
            return _response;
        }


        [AllowAnonymous]
        [Route("api/resetpassword")]
        [HttpPost]
        public async Task<IResponseDTO> ResetPassword([FromBody] ResetPasswordParamsDto options)
        {
            _response = await _accountService.ResetPassword(options);
            return _response;
        }

        [Route("api/me/changepassword")]
        [HttpPost]
        public async Task<IResponseDTO> ChangePassword([FromBody] ChangePasswordParamsDto options)
        {
            _response = await _accountService.ChangePassword(LoggedInUserId, options);
            return _response;
        }

        [Route("api/me/updateprofile")]
        [HttpPut]
        public async Task<IResponseDTO> UpdateUserProfile([ModelBinder(BinderType = typeof(JsonModelBinder))] UpdateUserProfile options)
        {
            var file = Request.Form?.Files?.Count > 0 ? Request.Form?.Files[0] : null;
            _response = await _accountService.UpdateUserProfile(LoggedInUserId, options, file);
            return _response;
        }


        [Route("api/information/me")]
        [HttpGet]
        public IResponseDTO GetLoggedinUserInfo()
        {
            _response = _accountService.GetUser(ServerRootPath, LoggedInUserId);
            return _response;
        }


        [Authorize(Roles = "Super Admin, Local Admin")]
        [Route("api/users")]
        [HttpGet]
        public IResponseDTO SearchUsers([FromQuery] UserFilterDto filterDto)
        {
            _response = _accountService.SearchUsers(filterDto);
            return _response;
        }


        [Authorize(Roles = "Super Admin, Local Admin")]
        [Route("api/users")]
        [HttpPost]
        public async Task<IResponseDTO> CreateUser([FromBody] CreateUpdateUserDto options)
        {
            _response = await _accountService.CreateUser(options, LoggedInUserId);
            return _response;
        }


        [Authorize(Roles = "Super Admin, Local Admin")]
        [Route("api/users/{id}")]
        [HttpPut]
        public async Task<IResponseDTO> UpdateUser([FromRoute] int id, [FromBody] CreateUpdateUserDto options)
        {
            _response = await _accountService.UpdateUser(id, options, LoggedInUserId);
            return _response;
        }


        [Authorize(Roles = "Super Admin, Local Admin")]
        [Route("api/users/{id}/deactivate")]
        [HttpPost]
        public async Task<IResponseDTO> DeactivateUser([FromRoute] int id)
        {
            if(id == LoggedInUserId)
            {
                _response.IsPassed = false;
                _response.Errors.Add("You can't deactivate yourself.");
                return _response;
            }
            _response = await _accountService.DeactivateUser(id, LoggedInUserId);
            return _response;
        }

        [Authorize(Roles = "Super Admin, Local Admin")]
        [Route("api/users/{id}/activate")]
        [HttpPost]
        public async Task<IResponseDTO> ActivateUser([FromRoute] int id)
        {
            if (id == LoggedInUserId)
            {
                _response.IsPassed = false;
                _response.Errors.Add("You can't activate yourself.");
                return _response;
            }
            _response = await _accountService.ActivateUser(id, LoggedInUserId);
            return _response;
        }

    }
}
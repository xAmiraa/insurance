using AutoMapper;
using InsuranceClaims.Core.Common;
using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.Data.DataContext;
using InsuranceClaims.Data.DbModels.SecuritySchema;
using InsuranceClaims.DTO.Security.User;
using InsuranceClaims.Enums;
using InsuranceClaims.Services.SendEmail;
using InsuranceClaims.Services.UploadFiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Security.Account
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IResponseDTO _response;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUploadFilesService _uploadFilesService;

        public AccountService(
          IConfiguration configuration,
          UserManager<ApplicationUser> userManager,
          IPasswordHasher<ApplicationUser> passwordHasher,
          IResponseDTO responseDTO,
          RoleManager<ApplicationRole> roleManager,
          IMapper mapper,
          IEmailService emailService,
          AppDbContext appDbContext,
          IUploadFilesService uploadFilesService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _response = responseDTO;
            _roleManager = roleManager;
            _mapper = mapper;
            _emailService = emailService;
            _appDbContext = appDbContext;
            _uploadFilesService = uploadFilesService;
        }
        

        public async Task<IResponseDTO> Login(LoginParamsDto loginParams)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(loginParams.Email);

                if (appUser == null)
                {
                    _response.Errors.Add("Email is not found");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.Locked.ToString())
                {
                    _response.Errors.Add("Your Account is locked. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Errors.Add("Your Account is disabled. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }

                if (appUser != null &&
                    _passwordHasher.VerifyHashedPassword(appUser, appUser.PasswordHash, loginParams.Password) !=
                    PasswordVerificationResult.Success)
                {
                    appUser.AccessFailedCount += 1;
                    await _userManager.UpdateAsync(appUser);

                    _response.Errors.Add("Invalid password");
                    _response.IsPassed = false;
                    return _response;
                }


                // in case user logged in successfully, reset AccessFailedCount
                if (appUser.AccessFailedCount > 0)
                {
                    appUser.AccessFailedCount = 0;
                    await _userManager.UpdateAsync(appUser);
                }

                var token = GenerateJSONWebToken(appUser.Id, appUser.UserName, appUser.CompanyId ?? 0);

                _response.IsPassed = true;
                _response.Message = "You are logged in successfully.";
                _response.Data = token;
            }
            catch (Exception ex)
            {
                _response.IsPassed = false;
                _response.Message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
                return _response;
            }

            return _response;
        }
        public async Task<IResponseDTO> ResetPassword(ResetPasswordParamsDto options)
        {
            try
            {
                if (string.IsNullOrEmpty(options.Email))
                {
                    _response.Errors.Add("Email is not found");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (string.IsNullOrEmpty(options.Token))
                {
                    _response.Errors.Add("Token is not valid");
                    _response.IsPassed = false;
                    return _response;
                }

                var appUser = await _userManager.FindByEmailAsync(options.Email.Trim());
                if (appUser == null)
                {
                    _response.Errors.Add("Email is not found");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.Locked.ToString())
                {
                    _response.Errors.Add("Your Account is locked. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Errors.Add("Your Account is disabled. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }

                // appUser.IsPasswordSet = true;
                var result = await _userManager.ResetPasswordAsync(appUser, options.Token, options.NewPassword);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Errors = result.Errors.Select(x => x.Description).ToList();
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Your password is setted successfully";
            }
            catch (Exception ex)
            {
                _response.IsPassed = false;
                _response.Message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
                return _response;
            }

            return _response;
        }
        public async Task<IResponseDTO> ChangePassword(int userId, ChangePasswordParamsDto options)
        {
            try
            {
                if (string.IsNullOrEmpty(options.CurrentPassword))
                {
                    _response.Errors.Add("Currnet password should not be empty");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (string.IsNullOrEmpty(options.NewPassword))
                {
                    _response.Errors.Add("New password should not be empty");
                    _response.IsPassed = false;
                    return _response;
                }

                var appUser = await _userManager.FindByIdAsync(userId.ToString());
                if (appUser == null)
                {
                    _response.Errors.Add("User is not found");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.Locked.ToString())
                {
                    _response.Errors.Add("Your Account is locked. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Errors.Add("Your Account is disabled. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }

                var result = await _userManager.ChangePasswordAsync(appUser, options.CurrentPassword, options.NewPassword);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Errors = result.Errors.Select(x => x.Description).ToList();
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Your password is changed successfully";
            }
            catch (Exception ex)
            {
                _response.IsPassed = false;
                _response.Message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
                return _response;
            }

            return _response;
        }
        public async Task<IResponseDTO> UpdateUserProfile(int id, UpdateUserProfile options, IFormFile file)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(id.ToString());

                if (appUser == null)
                {
                    _response.Errors.Add("User id is not exist.");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.Locked.ToString())
                {
                    _response.Errors.Add("Your Account is locked. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Errors.Add("Your Account is disabled. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }

                // Update user info
                appUser.FirstName = options.FirstName;
                appUser.LastName = options.LastName;
                appUser.Address = options.Address;
                appUser.UpdatedBy = id;
                appUser.UpdatedOn = DateTime.Now;

                var path = $"\\Uploads\\Users\\User_{id}";
                if(file != null)
                {
                    // Update User image path
                    appUser.PersonalImagePath = $"{path}\\{file?.FileName}";
                }

                // Update DB
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Errors = result.Errors.Select(x => x.Description).ToList();
                    return _response;
                }

                // Upload the documents
                if (file != null)
                {
                    await _uploadFilesService.UploadFile(path, file);
                }

                _response.IsPassed = true;
                _response.Message = "Your profile is updated successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }

        public IResponseDTO SearchUsers(UserFilterDto filterDto)
        {
            try
            {
                var query = _userManager.Users
                                .Include(x => x.UserRoles)
                                .AsQueryable();

                if (filterDto != null)
                {
                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        query = query.Where(x => x.FirstName.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()) || x.LastName.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Email))
                    {
                        query = query.Where(x => x.Email.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Address))
                    {
                        query = query.Where(x => x.Address.Trim().ToLower().Contains(filterDto.Address.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Status))
                    {
                        query = query.Where(x => x.Status == filterDto.Status);
                    }
                }

                //Check Sort Property
                if (!string.IsNullOrEmpty(filterDto?.SortProperty))
                {
                    //query = query.OrderBy(
                    //    string.Format("{0} {1}", filterDto.SortProperty, filterDto.IsAscending ? "ASC" : "DESC"));
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                // Pagination
                var total = query.Count();
                if (filterDto.PageIndex.HasValue && filterDto.PageSize.HasValue)
                {
                    query = query.Skip((filterDto.PageIndex.Value - 1) * filterDto.PageSize.Value).Take(filterDto.PageSize.Value);
                }

                var datalist = _mapper.Map<List<UserDto>>(query.ToList());

                foreach(var item in datalist)
                {
                    var roleId = query.FirstOrDefault(x => x.Id == item.Id).UserRoles.FirstOrDefault().RoleId;
                    var role = _roleManager.Roles.FirstOrDefault(x => x.Id == roleId);
                    item.Role = _mapper.Map<LookupDto>(role);
                }

                _response.IsPassed = true;
                _response.Data = new
                {
                    List = datalist,
                    Total = total,
                };
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public IResponseDTO GetUser(string rootPath, int id)
        {
            try
            {
                var user = _userManager.Users
                                .Include(x => x.UserRoles)
                                .FirstOrDefault(x => x.Id == id);

                var userDto = _mapper.Map<UserDto>(user);
                var roleId = user.UserRoles.FirstOrDefault().RoleId;
                var role = _roleManager.Roles.FirstOrDefault(x => x.Id == roleId);
                userDto.Role = _mapper.Map<LookupDto>(role);

                if(!string.IsNullOrEmpty(userDto.PersonalImagePath))
                {
                    userDto.PersonalImagePath = rootPath + userDto.PersonalImagePath;
                }

                _response.IsPassed = true;
                _response.Data = userDto;
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> CreateUser(CreateUpdateUserDto options, int userId)
        {
            try
            {
                // Generate user password
                var password = GeneratePassword();
                var appUser = _mapper.Map<ApplicationUser>(options);
                appUser.CreatedBy = userId;
                appUser.CreatedOn = DateTime.Now;
                appUser.UserName = appUser.Email;
                appUser.ChangePassword = true;
                appUser.Status = UserStatusEnum.Active.ToString();
                appUser.UserRoles = new List<ApplicationUserRole>
                {
                    new ApplicationUserRole
                    {
                        RoleId = options.RoleId
                    }
                };

                // Create the user
                IdentityResult result = await _userManager.CreateAsync(appUser, password);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Errors = result.Errors.Select(x => x.Description).ToList();
                    return _response;
                }

                // send email to the user to change his password
                var verifyEmailToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                verifyEmailToken = WebUtility.UrlEncode(verifyEmailToken);
                await _emailService.AfterRegistiration(appUser.Email, verifyEmailToken);

                _response.IsPassed = true;
                _response.Message = "Email was sent to the user email so he can set his password.";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> UpdateUser(int id, CreateUpdateUserDto options, int userId)
        {
            try
            {
                var appUser = _userManager.Users
                                .Include(x => x.UserRoles)
                                .FirstOrDefault(x => x.Id == id);

                if (appUser == null)
                {
                    _response.Errors.Add("User id is not exist.");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.Locked.ToString())
                {
                    _response.Errors.Add("Your Account is locked. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Errors.Add("Your Account is disabled. Please contact your administration");
                    _response.IsPassed = false;
                    return _response;
                }

                // Update user info
                appUser.FirstName = options.FirstName;
                appUser.LastName = options.LastName;
                appUser.Address = options.Address;
                appUser.UpdatedBy = userId;
                appUser.UpdatedOn = DateTime.Now;

                // Update DB
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Errors = result.Errors.Select(x => x.Description).ToList();
                    return _response;
                }

                // Update the role
                var oldRoleId = appUser.UserRoles.FirstOrDefault().RoleId;
                if (options.RoleId != oldRoleId)
                {
                    var oldUserRole = _appDbContext.UserRoles.FirstOrDefault(x => x.Id == appUser.UserRoles.FirstOrDefault().Id);
                    var newUserRole = new ApplicationUserRole
                    {
                        User = appUser,
                        UserId = id,
                        RoleId = options.RoleId
                    };

                    _appDbContext.UserRoles.Remove(oldUserRole);
                    await _appDbContext.UserRoles.AddAsync(newUserRole);

                    // save to the database
                    var save = await _appDbContext.SaveChangesAsync();
                    if (save == 0)
                    {
                        _response.IsPassed = false;
                        _response.Errors.Add("Database did not save the object");
                        return _response;
                    }
                }

                _response.IsPassed = true;
                _response.Message = "User is updated successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> DeactivateUser(int id, int userId)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(id.ToString());

                if (appUser == null)
                {
                    _response.Errors.Add("User id is not exist.");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Errors.Add("User Account is already disabled.");
                    _response.IsPassed = false;
                    return _response;
                }

                // Update user info
                appUser.Status = UserStatusEnum.NotActive.ToString();
                appUser.UpdatedBy = userId;
                appUser.UpdatedOn = DateTime.Now;

                // Update DB
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Errors = result.Errors.Select(x => x.Description).ToList();
                    return _response;
                }
                
                _response.IsPassed = true;
                _response.Message = "User account is deactivated successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> ActivateUser(int id, int userId)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(id.ToString());

                if (appUser == null)
                {
                    _response.Errors.Add("User id is not exist.");
                    _response.IsPassed = false;
                    return _response;
                }
                else if (appUser.Status == UserStatusEnum.Active.ToString())
                {
                    _response.Errors.Add("User Account is already activated.");
                    _response.IsPassed = false;
                    return _response;
                }

                // Update user info
                appUser.Status = UserStatusEnum.Active.ToString();
                appUser.UpdatedBy = userId;
                appUser.UpdatedOn = DateTime.Now;

                // Update DB
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Errors = result.Errors.Select(x => x.Description).ToList();
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "User account is activated successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }

        private string GenerateJSONWebToken(int userId, string userName, int companyId)
        {
            var signingKey = Convert.FromBase64String(_configuration["Jwt:Key"]);
            var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);

            var claims = new List<Claim>
            {
                new Claim("userid", userId.ToString()),
                new Claim("companyid", companyId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName)
            };

            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var item in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, item);
                claims.Add(roleClaim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
        private string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            var result = Regex.Replace(password.ToString(), @"[^0-9a-zA-Z]+", "$");
            result += RandomString(6);

            return result;
        }
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdrfghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

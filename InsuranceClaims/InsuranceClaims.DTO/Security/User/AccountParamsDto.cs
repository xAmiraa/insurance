
namespace InsuranceClaims.DTO.Security.User
{
    public class LoginParamsDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class ResetPasswordParamsDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordParamsDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangeEmailParamsDto
    {
        public int UserId { get; set; }
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
    }

    public class ChangeUserStatusParamsDto
    {
        public int UserId { get; set; }
        public string Status { get; set; }
    }

    public class ForgetPassParamsDto
    {
        public string Email { get; set; }
    }
    public class UpdateUserProfile
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}

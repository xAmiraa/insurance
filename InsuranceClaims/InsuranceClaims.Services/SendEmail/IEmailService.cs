using InsuranceClaims.Core.Common;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.SendEmail
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);

        Task AfterRegistiration(string email, string vaildToken);
        Task RequestToResetPassword(string email, string validToken);
    }
}

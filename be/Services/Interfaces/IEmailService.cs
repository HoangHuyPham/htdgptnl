using be.Models;

namespace be.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendChangeEmail(User user, string address, string newEmail);
        public Task SendResetPassword(User user, string address, string newEmail);
    }
}
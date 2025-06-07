using be.DTOs.User;
using be.Models;

namespace be.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User?> Login(string username, string password);
        public Task<User?> CreateUser(CreateUserDTO createDTO);
        public Task<User?> ResetPassword(string username, string? password, string? newPassword);
        public string? GenerateJWTToken(User user);
    }
}
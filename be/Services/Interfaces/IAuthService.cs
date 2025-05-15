using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User?> Login(string username, string password);
        public Task<User?> CreateUser(string username, string password);
        public Task<User?> ResetPassword(string username, string? password, string? newPassword);
        public string? GenerateJWTToken(User user);
    }
}
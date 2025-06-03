using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.DTOs.Role;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using be.Models;
using be.Repos;
using be.Repos.Interfaces;
using be.Services.Interfaces;

namespace be.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using BCrypt.Net;
    using be.DTOs.Role;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.JsonWebTokens;
    using Microsoft.IdentityModel.Tokens;

    public class AuthService(IUserRepository _userRepository, IConfiguration _configuration) : IAuthService
    {
        private readonly IUserRepository userRepository = _userRepository;
        private readonly IConfiguration configuration = _configuration;
        public async Task<User?> Login(string username, string password)
        {
            var existUser = await userRepository.FindByUserName(username);

            if (existUser == null)
            {
                return null;
            }

            if (!BCrypt.Verify(password, existUser.Password))
            {
                return null;
            }

            return existUser;
        }

        public async Task<User?> CreateUser(CreateUserDTO createDTO)
        {
            var existUser = await userRepository.FindByUserName(createDTO.UserName);

            if (existUser != null){
                return null;
            }

            var newUser = await userRepository.Create(new User
            {
                UserName = createDTO.UserName,
                Password = BCrypt.HashPassword(createDTO.Password),
                Email = createDTO.Email,
                Phone = createDTO.Phone,
                RoleId = createDTO.RoleId,
            });

            return newUser;
        }

        public async Task<User?> ResetPassword(string username, string? password, string? newPassword = "1234")
        {
            var existUser = await userRepository.FindByUserName(username);
            
            if (existUser == null){
                return null;
            }

            if (!BCrypt.Verify(password, existUser.Password)){
                return null;
            }

            existUser.Password = BCrypt.HashPassword(newPassword);
            return await userRepository.Update(existUser);
        }

        public string? GenerateJWTToken(User user){
            if (user.RoleId == null) return null;

            List<Claim> claims = [
                new Claim("sub", user.Id.ToString()),
                new Claim("roleId", user.RoleId.ToString()!),
            ];

            var symKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTSecret:Key").Value!));
            var cred = new SigningCredentials(symKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: cred
            );

            var normalizeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return normalizeToken;
        }
    }
}
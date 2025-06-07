using be.DTOs.Auth;
using be.DTOs.Criteria;
using be.DTOs.User;
using be.Helpers;
using be.Mappers;
using be.Models;
using be.Repos.Interfaces;
using be.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        private readonly IAuthService authService = _authService;
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var User = await authService.Login(dto.Username, dto.Password);

            if (User == null)
                return Ok(new ApiResponse<User>
                {
                    Message = "login failed",
                    Data = null
                });

            var jwtToken = authService.GenerateJWTToken(User);

            return Ok(new ApiResponse<string>
            {
                Message = "login success",
                Data = jwtToken,
            });
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            if (dto.OldPassword == dto.Password)
            {
                return Forbid();
            }

            var User = await authService.ResetPassword(dto.Username, dto.OldPassword, dto.Password);

            if (User == null)
            {
                return Forbid();
            }

            var jwtToken = authService.GenerateJWTToken(User);

            return Ok(new ApiResponse<string>
            {
                Message = "change password success",
                Data = jwtToken,
            });
        }
    }
}
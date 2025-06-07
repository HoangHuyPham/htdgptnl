using be.Helpers;
using be.Mappers;
using be.Models;
using be.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using be.DTOs.User;
using Microsoft.IdentityModel.Tokens;
using be.DTOs.Profile;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController(IUserRepository _repoUser, IEmailService emailService) : ControllerBase
    {
        private readonly IUserRepository repoUser = _repoUser;
        private readonly IEmailService emailService = emailService;

        [HttpGet("Me")]
        public async Task<IActionResult> Me()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existUser = await repoUser.FindById(Guid.Parse(userId ?? Guid.Empty.ToString()));

            if (existUser == null)
            {
                return NotFound();
            }

            return Ok(new ApiResponse<UserInfoDTO>
            {
                Data = existUser.getUserInfoDTO(),
                Message = "success"
            });
        }

        [HttpPost("ChangePhone")]
        public async Task<IActionResult> ChangePhone([FromBody] ChangePhoneDTO dto)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existUser = await repoUser.FindById(Guid.Parse(userId ?? Guid.Empty.ToString()));

            if (existUser == null)
            {
                return NotFound("entity not found");
            }

            existUser.Phone = dto.NewPhone;
            await repoUser.Update(existUser);

            return Ok(new ApiResponse<UserInfoDTO>
            {
                Data = existUser.getUserInfoDTO(),
                Message = "success"
            });
        }

        [HttpPost("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDTO dto)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existUser = await repoUser.FindById(Guid.Parse(userId ?? Guid.Empty.ToString()));

            if (existUser == null)
            {
                return NotFound("entity not found");
            }

            if (existUser.Email != null && !existUser.Email.IsNullOrEmpty())
            {
                await emailService.SendChangeEmail(existUser, existUser.Email, dto.NewEmail);
                return NoContent();
            }

            existUser.Email = dto.NewEmail;
            await repoUser.Update(existUser);

            return Ok(new ApiResponse<UserInfoDTO>
            {
                Data = existUser.getUserInfoDTO(),
                Message = "success"
            });
        }
    }
}
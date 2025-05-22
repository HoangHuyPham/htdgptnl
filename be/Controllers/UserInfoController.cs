using System.Security.Claims;
using System.Transactions;
using be.Contexts;
using be.DTOs.User;
using be.Helpers;
using be.Mappers;
using be.Models;
using be.Repos.Interfaces;
using be.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController(IUserRepository _UserRepo ) : ControllerBase
    {
        private readonly IUserRepository UserRepo = _UserRepo;

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var id = HttpContext.User.FindFirstValue("id");
            if (id == null) return Ok(new ApiResponse<User>
            {
                Message = "Not found id!",
                Data = null
            });

            var existUser = await UserRepo.FindById(Guid.Parse(id));
            if (existUser == null) return Ok(new ApiResponse<User>
            {
                Message = "Not found user!",
                Data = null
            });

            return Ok(new ApiResponse<UserInfoDTO>
            {
                Message = "success",
                Data = existUser.getUserInfoDTO()
            });
        }
    }
}
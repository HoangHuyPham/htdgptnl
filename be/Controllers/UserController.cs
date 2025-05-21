using System.Transactions;
using be.Contexts;
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
    public class UserController(IUserRepository _UserRepo, IAuthService _authService, ApplicationDbContext _dbContext) : ControllerBase
    {
        private readonly IUserRepository UserRepo = _UserRepo;
        private readonly IAuthService authService = _authService;
        private readonly ApplicationDbContext dbContext = _dbContext;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var Users = await UserRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<User>>
            {
                Message = "get success",
                Data = Users,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var newUser = await authService.CreateUser(dto.Username, dto.Password);

                if (newUser == null)
                {
                    return Ok(new ApiResponse<User>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                newUser.EmployeeId = dto.EmployeeId;
                newUser.RoleId = dto.RoleId;
                newUser.Email = dto.Email;
                newUser.Phone = dto.Phone;

                var savedUser = await UserRepo.Update(newUser);

                if (savedUser == null)
                {
                    return Ok(new ApiResponse<User>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                await transaction.CommitAsync();
                
                return Ok(new ApiResponse<User>
                {
                    Message = "create success",
                    Data = savedUser,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<User>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PutUserDTO dto)
        {
            try
            {
                var User = await UserRepo.FindById(id);

                if (User == null)
                {
                    return NotFound(new ApiResponse<User>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                User.EmployeeId = dto.EmployeeId;
                User.RoleId = dto.RoleId;
                User.Email = dto.Email;
                User.Phone = dto.Phone;

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<User>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<User>
                {
                    Message = "update success",
                    Data = await UserRepo.Update(User),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<User>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<User> jsonPatch)
        {
            try
            {
                var User = await UserRepo.FindById(id);
                if (User == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<User>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(User, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<User>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<User>
                {
                    Message = "update success",
                    Data = await UserRepo.Update(User),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<User>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await UserRepo.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateUserDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateUserDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<User>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
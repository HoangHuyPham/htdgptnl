using be.Helpers;
using be.Models;
using be.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using be.DTOs.User;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserRepository _repoUser, IAuthService _authService) : ControllerBase
    {
        private readonly IUserRepository repoUser = _repoUser;
        private readonly IAuthService authService = _authService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query)
        {
            return Ok(await repoUser.FindAll(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            try
            {
                var result = await authService.CreateUser(dto);

                if (result == null)
                {
                    return Ok(new ApiResponse<User>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<User>
                {
                    Message = "create success",
                    Data = result,
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
        public async Task<IActionResult> Put(Guid id, CreateUserDTO dto)
        {
            try
            {

                var User = await repoUser.FindById(id);

                if (User == null)
                {
                    return NotFound(new ApiResponse<User>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                User.UserName = dto.UserName;
                User.Email = dto.Email;
                User.Phone = dto.Phone;
                User.RoleId = dto.RoleId;

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
                    Data = await repoUser.Update(User),
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
                var User = await repoUser.FindById(id);
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
                    Data = await repoUser.Update(User),
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
                var result = await repoUser.Delete(id);

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
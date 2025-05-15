using be.DTOs.Role;
using be.Helpers;
using be.Mappers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(IRepository<Role> _RoleRepo) : ControllerBase
    {
        private readonly IRepository<Role> RoleRepo = _RoleRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var Roles = await RoleRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<RoleDTO>>
            {
                Message = "get success",
                Data = Roles.Select(x=>x.getDTO()).ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleDTO dto)
        {
            try
            {
                var result = await RoleRepo.Create(new Role
                {
                    Name = dto.Name,
                    Description = dto.Description,
                });

                if (result == null)
                {
                    return Ok(new ApiResponse<Role>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<Role>
                {
                    Message = "create success",
                    Data = result,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Role>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PutRoleDTO dto)
        {
            try
            {

                var Role = await RoleRepo.FindById(id);

                if (Role == null)
                {
                    return NotFound(new ApiResponse<Role>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }
                
                Role.Description = dto.Description;
                Role.Name = dto.Name;

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<Role>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<Role>
                {
                    Message = "update success",
                    Data = await RoleRepo.Update(Role),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Role>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Role> jsonPatch)
        {
            try
            {
                var Role = await RoleRepo.FindById(id);
                if (Role == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<Role>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(Role, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<Role>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<Role>
                {
                    Message = "update success",
                    Data = await RoleRepo.Update(Role),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Role>
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
                var result = await RoleRepo.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateRoleDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateRoleDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Role>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
using be.DTOs.RoleType;
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
    public class RoleTypeController(IRepository<RoleType> _RoleTypeRepo) : ControllerBase
    {
        private readonly IRepository<RoleType> RoleTypeRepo = _RoleTypeRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var RoleTypes = await RoleTypeRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<RoleTypeDTO>>
            {
                Message = "get success",
                Data = RoleTypes.Select(x=>x.getDTO()).ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleTypeDTO dto)
        {
            try
            {
                var result = await RoleTypeRepo.Create(new RoleType
                {
                    Name = dto.Name,
                });

                if (result == null)
                {
                    return Ok(new ApiResponse<RoleType>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<RoleType>
                {
                    Message = "create success",
                    Data = result,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<RoleType>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PutRoleTypeDTO dto)
        {
            try
            {

                var RoleType = await RoleTypeRepo.FindById(id);

                if (RoleType == null)
                {
                    return NotFound(new ApiResponse<RoleType>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }
                
                RoleType.Name = dto.Name;

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<RoleType>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<RoleType>
                {
                    Message = "update success",
                    Data = await RoleTypeRepo.Update(RoleType),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<RoleType>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<RoleType> jsonPatch)
        {
            try
            {
                var RoleType = await RoleTypeRepo.FindById(id);
                if (RoleType == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<RoleType>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(RoleType, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<RoleType>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<RoleType>
                {
                    Message = "update success",
                    Data = await RoleTypeRepo.Update(RoleType),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<RoleType>
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
                var result = await RoleTypeRepo.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateRoleTypeDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateRoleTypeDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<RoleType>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
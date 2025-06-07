using be.DTOs.Criteria;
using be.DTOs.User;
using be.Helpers;
using be.Mappers;
using be.Models;
using be.Repos.Interfaces;
using be.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using be.DTOs.Role;
using System.Security.Claims;
using be.DTOs.EvaluationScore;
using be.DTOs.EvaluationSchedule;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationScheduleController(IEvaluationScheduleRepository _repoEvaluationSchedule) : ControllerBase
    {
        private readonly IEvaluationScheduleRepository repoEvaluationSchedule = _repoEvaluationSchedule;

        [HttpGet("Self")]
        public async Task<IActionResult> Self()
        {
            var roleId = HttpContext.User.FindFirstValue("roleId");
            var availableSchedules = await repoEvaluationSchedule.FindAllAvailable();

            return Ok(new ApiResponse<List<EvaluationSchedule>>
            {
                Message = "success",
                Data = availableSchedules.Where(x => (x.RoleId == Guid.Parse(roleId ?? Guid.Empty.ToString())) && x.IsSelfEvalution).ToList(),
            });
        }

        [HttpGet("Available")]
        public async Task<IActionResult> Available()
        {
            var roleId = HttpContext.User.FindFirstValue("roleId");
            var availableSchedules = await repoEvaluationSchedule.FindAllAvailable();

            return Ok(new ApiResponse<List<EvaluationSchedule>>
            {
                Message = "success",
                Data = availableSchedules.Where(x => x.RoleId == Guid.Parse(roleId ?? Guid.Empty.ToString())).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query)
        {
            return Ok(await repoEvaluationSchedule.FindAll(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEvaluationScheduleDTO dto)
        {
            try
            {
                var result = await repoEvaluationSchedule.Create(new EvaluationSchedule
                {
                    Description = dto.Description,
                    Start = dto.Start,
                    End = dto.End,
                    RoleId = dto.RoleId,
                    PerformanceEvaluationId = dto.PerformanceEvaluationId,
                });

                if (result == null)
                {
                    return Ok(new ApiResponse<EvaluationSchedule>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<EvaluationSchedule>
                {
                    Message = "create success",
                    Data = result,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluationSchedule>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CreateEvaluationScheduleDTO dto)
        {
            try
            {
                var EvaluationSchedule = await repoEvaluationSchedule.FindById(id);

                if (EvaluationSchedule == null)
                {
                    return NotFound(new ApiResponse<EvaluationSchedule>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                EvaluationSchedule.Description = dto.Description;
                EvaluationSchedule.Start = dto.Start;
                EvaluationSchedule.End = dto.End;
                EvaluationSchedule.RoleId = dto.RoleId;
                EvaluationSchedule.PerformanceEvaluationId = dto.PerformanceEvaluationId;

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<EvaluationSchedule>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<EvaluationSchedule>
                {
                    Message = "update success",
                    Data = await repoEvaluationSchedule.Update(EvaluationSchedule),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluationSchedule>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<EvaluationSchedule> jsonPatch)
        {
            try
            {
                var EvaluationSchedule = await repoEvaluationSchedule.FindById(id);
                if (EvaluationSchedule == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<EvaluationSchedule>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(EvaluationSchedule, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<EvaluationSchedule>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<EvaluationSchedule>
                {
                    Message = "update success",
                    Data = await repoEvaluationSchedule.Update(EvaluationSchedule),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluationSchedule>
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
                var result = await repoEvaluationSchedule.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateEvaluationScheduleDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateEvaluationScheduleDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluationSchedule>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
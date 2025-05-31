using be.DTOs.EvaluationSchedule;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationScheduleController(IRepository<EvaluationSchedule> _evaluationScheduleRepo) : ControllerBase
    {
        private readonly IRepository<EvaluationSchedule> evaluationScheduleRepo = _evaluationScheduleRepo;
    
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var evaluationSchedules = await evaluationScheduleRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<EvaluationSchedule>>
            {
                Message = "get success",
                Data = evaluationSchedules,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEvaluationScheduleDTO dto)
        {
            try
            {
                var result = await evaluationScheduleRepo.Create(new EvaluationSchedule
                {
                    PerformanceEvaluationId = dto.PerformanceEvaluationId,
                    ScheduleId = dto.ScheduleId
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
        public async Task<IActionResult> Put(Guid id, PutEvaluationScheduleDTO dto)
        {
            try
            {

                var evaluationSchedule = await evaluationScheduleRepo.FindById(id);

                if (evaluationSchedule == null)
                {
                    return NotFound(new ApiResponse<EvaluationSchedule>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                evaluationSchedule.PerformanceEvaluationId = dto.PerformanceEvaluationId;
                evaluationSchedule.ScheduleId = dto.ScheduleId;

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
                    Data = await evaluationScheduleRepo.Update(evaluationSchedule),
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
                var evaluationSchedule = await evaluationScheduleRepo.FindById(id);
                if (evaluationSchedule == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<EvaluationSchedule>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(evaluationSchedule, ModelState);

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
                    Data = await evaluationScheduleRepo.Update(evaluationSchedule),
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
                var result = await evaluationScheduleRepo.Delete(id);

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
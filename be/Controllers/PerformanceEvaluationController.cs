using be.DTOs.PerformanceEvaluation;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceEvaluationController(IRepository<PerformanceEvaluation> _PerformanceEvaluationRepo) : ControllerBase
    {
        private readonly IRepository<PerformanceEvaluation> PerformanceEvaluationRepo = _PerformanceEvaluationRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var PerformanceEvaluations = await PerformanceEvaluationRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<PerformanceEvaluation>>
            {
                Message = "get success",
                Data = PerformanceEvaluations,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePerformanceEvaluationDTO dto)
        {
            try
            {
                var result = await PerformanceEvaluationRepo.Create(new PerformanceEvaluation
                {
                    Name = dto.Name,
                });

                if (result == null)
                {
                    return Ok(new ApiResponse<PerformanceEvaluation>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<PerformanceEvaluation>
                {
                    Message = "create success",
                    Data = result,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<PerformanceEvaluation>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PutPerformanceEvaluationDTO dto)
        {
            try
            {

                var PerformanceEvaluation = await PerformanceEvaluationRepo.FindById(id);

                if (PerformanceEvaluation == null)
                {
                    return NotFound(new ApiResponse<PerformanceEvaluation>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }
                
                PerformanceEvaluation.Name = dto.Name;

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<PerformanceEvaluation>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<PerformanceEvaluation>
                {
                    Message = "update success",
                    Data = await PerformanceEvaluationRepo.Update(PerformanceEvaluation),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<PerformanceEvaluation>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<PerformanceEvaluation> jsonPatch)
        {
            try
            {
                var PerformanceEvaluation = await PerformanceEvaluationRepo.FindById(id);
                if (PerformanceEvaluation == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<PerformanceEvaluation>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(PerformanceEvaluation, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<PerformanceEvaluation>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<PerformanceEvaluation>
                {
                    Message = "update success",
                    Data = await PerformanceEvaluationRepo.Update(PerformanceEvaluation),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<PerformanceEvaluation>
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
                var result = await PerformanceEvaluationRepo.Delete(id);

                if (!result) return Ok(new ApiResponse<CreatePerformanceEvaluationDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreatePerformanceEvaluationDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<PerformanceEvaluation>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
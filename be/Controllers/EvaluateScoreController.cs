using be.DTOs.EvaluateScore;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluateScoreController(IRepository<EvaluateScore> _EvaluateScoreRepo) : ControllerBase
    {
        private readonly IRepository<EvaluateScore> EvaluateScoreRepo = _EvaluateScoreRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var EvaluateScores = await EvaluateScoreRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<EvaluateScore>>
            {
                Message = "get success",
                Data = EvaluateScores,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEvaluateScoreDTO dto)
        {
            try
            {
                var result = await EvaluateScoreRepo.Create(new EvaluateScore
                {
                    Score = dto.Score,
                    EmployeeId = dto.EmployeeId,
                    CriteriaId = dto.CriteriaId,
                });

                if (result == null)
                {
                    return Ok(new ApiResponse<EvaluateScore>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<EvaluateScore>
                {
                    Message = "create success",
                    Data = result,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluateScore>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PutEvaluateScoreDTO dto)
        {
            try
            {

                var EvaluateScore = await EvaluateScoreRepo.FindById(id);

                if (EvaluateScore == null)
                {
                    return NotFound(new ApiResponse<EvaluateScore>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }
                
                EvaluateScore.Score = dto.Score;

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<EvaluateScore>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<EvaluateScore>
                {
                    Message = "update success",
                    Data = await EvaluateScoreRepo.Update(EvaluateScore),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluateScore>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<EvaluateScore> jsonPatch)
        {
            try
            {
                var EvaluateScore = await EvaluateScoreRepo.FindById(id);
                if (EvaluateScore == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<EvaluateScore>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(EvaluateScore, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<EvaluateScore>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<EvaluateScore>
                {
                    Message = "update success",
                    Data = await EvaluateScoreRepo.Update(EvaluateScore),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluateScore>
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
                var result = await EvaluateScoreRepo.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateEvaluateScoreDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateEvaluateScoreDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluateScore>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
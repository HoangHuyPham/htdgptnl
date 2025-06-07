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
using be.DTOs.EvaluationScore;
using System.Security.Claims;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationScoreController(IEvaluationScoreRepository _repoEvaluationScore, IRepository<Evidence> _repoEvidence) : ControllerBase
    {
        private readonly IEvaluationScoreRepository repoEvaluationScore = _repoEvaluationScore;
        private readonly IRepository<Evidence> repoEvidence = _repoEvidence;

        [HttpGet("Self")]
        public async Task<IActionResult> GetAllSelf(Guid? criteriaId)
        {
            var sourceId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (sourceId == null)
            {
                return Ok(new ApiResponse<EvaluationScore>
                {
                    Message = "source not found",
                    Data = null,
                });
            }

            List<EvaluationScore> result = [];

            if (criteriaId != null)
            {
                result.AddRange(await repoEvaluationScore.FindAllBy(sourceId: Guid.Parse(sourceId), targetId: Guid.Parse(sourceId), criteriaId: criteriaId));
            }
            else
            {
                result.AddRange(await repoEvaluationScore.FindAllBy(sourceId: Guid.Parse(sourceId), targetId: Guid.Parse(sourceId), criteriaId: null));
            }

            return Ok(new ApiResponse<List<EvaluationScore>>
            {
                Message = "success",
                Data = result.OrderByDescending(x=>x.CreatedAt).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query)
        {
            return Ok(await repoEvaluationScore.FindAll(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEvaluationScoreDTO dto)
        {
            try
            {
                var sourceId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (sourceId == null)
                {
                    return NotFound(new ApiResponse<EvaluationScore>
                    {
                        Message = "source not found",
                        Data = null,
                    });
                }

                var result = await repoEvaluationScore.Create(new EvaluationScore
                {
                    Score = dto.Score,
                    Comment = dto.Comment,
                    SourceId = Guid.Parse(sourceId),
                    TargetId = dto.TargetId,
                    CriteriaId = dto.CriteriaId,
                });

                if (result == null)
                {
                    return Forbid("create failed");
                }

                return Ok(new ApiResponse<EvaluationScore>
                {
                    Message = "create success",
                    Data = result,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluationScore>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CreateEvaluationScoreDTO dto)
        {
            try
            {
                var EvaluationScore = await repoEvaluationScore.FindById(id);
                var sourceId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (sourceId == null)
                {
                    return Ok(new ApiResponse<EvaluationScore>
                    {
                        Message = "source not found",
                        Data = null,
                    });
                }

                if (EvaluationScore == null)
                {
                    return NotFound(new ApiResponse<EvaluationScore>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                EvaluationScore.Score = dto.Score;
                EvaluationScore.Comment = dto.Comment;
                EvaluationScore.SourceId = Guid.Parse(sourceId);
                EvaluationScore.TargetId = dto.TargetId;
                EvaluationScore.CriteriaId = dto.CriteriaId;

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<EvaluationScore>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<EvaluationScore>
                {
                    Message = "update success",
                    Data = await repoEvaluationScore.Update(EvaluationScore),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluationScore>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<EvaluationScore> jsonPatch)
        {
            try
            {
                var EvaluationScore = await repoEvaluationScore.FindById(id);
                if (EvaluationScore == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<EvaluationScore>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(EvaluationScore, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<EvaluationScore>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<EvaluationScore>
                {
                    Message = "update success",
                    Data = await repoEvaluationScore.Update(EvaluationScore),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluationScore>
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
                var result = await repoEvaluationScore.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateEvaluationScoreDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateEvaluationScoreDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<EvaluationScore>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
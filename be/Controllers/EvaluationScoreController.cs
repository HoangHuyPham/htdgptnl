using System.Security.Claims;
using be.DTOs.EvaluationScore;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using be.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationScoreController(IRepository<Criteria> _CriteriaRepo, IRepository<Employee> _EmployeeRepo, IEvaluationScoreRepository _EvaluationScoreRepo, IUserRepository _UserRepo, IScoreService _ScoreService) : ControllerBase
    {
        private readonly IEvaluationScoreRepository EvaluationScoreRepo = _EvaluationScoreRepo;
        private readonly IRepository<Employee> EmployeeRepo = _EmployeeRepo;
        private readonly IRepository<Criteria> CriteriaRepo = _CriteriaRepo;
        private readonly IUserRepository UserRepo = _UserRepo;
        private readonly IScoreService ScoreService = _ScoreService;

        [HttpGet("self")]
        public async Task<IActionResult> GetAllBySelf(Guid criteriaId)
        {
            var UserId = HttpContext.User.FindFirstValue("id");

            if (UserId == null)
            {
                return Ok(new ApiPaginationResponse<RoleSchedule>
                {
                    Message = "get failed (jwt claims)",
                    Data = null,
                });
            }

            var UserExist = await UserRepo.FindById(Guid.Parse(UserId));

            if (UserExist == null)
            {
                return Ok(new ApiPaginationResponse<RoleSchedule>
                {
                    Message = "user not found",
                    Data = null,
                });
            }

            var EvaluationScores = await EvaluationScoreRepo.FindAllByUserId(UserExist.Id);

            return Ok(new ApiPaginationResponse<List<EvaluationScore>>
            {
                Message = "get success",
                Data = EvaluationScores.FindAll(x=>x.CriteriaId == criteriaId).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var EvaluationScores = await EvaluationScoreRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<EvaluationScore>>
            {
                Message = "get success",
                Data = EvaluationScores,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEvaluationScoreDTO dto)
        {
            try
            {
                var UserId = HttpContext.User.FindFirstValue("id");

                if (UserId == null)
                {
                    return Ok(new ApiPaginationResponse<EvaluationScore>
                    {
                        Message = "get failed (jwt claims)",
                        Data = null,
                    });
                }

                var UserExist = await UserRepo.FindById(Guid.Parse(UserId));

                if (UserExist == null)
                {
                    return Ok(new ApiPaginationResponse<EvaluationScore>
                    {
                        Message = "source not found",
                        Data = null,
                    });
                }

                var EvaluationScore = await ScoreService.AddScore(dto.Score!.Value, UserExist.Id, dto.TargetId!.Value, dto.CriteriaId!.Value);

                if (EvaluationScore == null)
                {
                    return Ok(new ApiResponse<EvaluationScore>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<EvaluationScore>
                {
                    Message = "create success",
                    Data = EvaluationScore,
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
        public async Task<IActionResult> Put(Guid id, PutEvaluationScoreDTO dto)
        {
            try
            {

                var EvaluationScore = await EvaluationScoreRepo.FindById(id);

                if (EvaluationScore == null)
                {
                    return NotFound(new ApiResponse<EvaluationScore>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                EvaluationScore.Score = dto.Score;

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
                    Data = await EvaluationScoreRepo.Update(EvaluationScore),
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
                var EvaluationScore = await EvaluationScoreRepo.FindById(id);
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
                    Data = await EvaluationScoreRepo.Update(EvaluationScore),
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
                var result = await EvaluationScoreRepo.Delete(id);

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
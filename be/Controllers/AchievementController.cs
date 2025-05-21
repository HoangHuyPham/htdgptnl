using be.DTOs.Achievement;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementController(IRepository<Achievement> _AchievementRepo) : ControllerBase
    {
        private readonly IRepository<Achievement> AchievementRepo = _AchievementRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var Achievements = await AchievementRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<Achievement>>
            {
                Message = "get success",
                Data = Achievements,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAchievementDTO dto)
        {
            try
            {
                var result = await AchievementRepo.Create(new Achievement
                {
                    Name = dto.Name,
                    PerformanceEvaluationId = dto.PerformanceEvaluationId,
                    TotalWeight = dto.TotalWeight
                });

                if (result == null)
                {
                    return Ok(new ApiResponse<Achievement>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<Achievement>
                {
                    Message = "create success",
                    Data = result,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Achievement>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PutAchievementDTO dto)
        {
            try
            {

                var Achievement = await AchievementRepo.FindById(id);

                if (Achievement == null)
                {
                    return NotFound(new ApiResponse<Achievement>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }
                
                Achievement.TotalWeight = dto.TotalWeight;
                Achievement.Name = dto.Name;
                Achievement.PerformanceEvaluationId = dto.PerformanceEvaluationId;

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<Achievement>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<Achievement>
                {
                    Message = "update success",
                    Data = await AchievementRepo.Update(Achievement),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Achievement>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Achievement> jsonPatch)
        {
            try
            {
                var Achievement = await AchievementRepo.FindById(id);
                if (Achievement == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<Achievement>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(Achievement, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<Achievement>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<Achievement>
                {
                    Message = "update success",
                    Data = await AchievementRepo.Update(Achievement),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Achievement>
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
                var result = await AchievementRepo.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateAchievementDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateAchievementDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Achievement>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
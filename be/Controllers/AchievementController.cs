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


namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementController(IRepository<Achievement> _repoAchievement) : ControllerBase
    {
        private readonly IRepository<Achievement> repoAchievement = _repoAchievement;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query)
        {
            return Ok(await repoAchievement.FindAll(query));
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] CreateAchievementDTO dto)
        // {
        //     try
        //     {
        //         var result = await repoAchievement.Create(new Achievement
        //         {
        //             Name = dto.Name,
        //             Description = dto.Description,
        //             Level = dto.Level
        //         });

        //         if (result == null)
        //         {
        //             return Ok(new ApiResponse<Achievement>
        //             {
        //                 Message = "create failed",
        //                 Data = null,
        //             });
        //         }

        //         return Ok(new ApiResponse<Achievement>
        //         {
        //             Message = "create success",
        //             Data = result,
        //         });
        //     }
        //     catch
        //     {
        //         return BadRequest(new ApiResponse<Achievement>
        //         {
        //             Message = "server error",
        //             Data = null,
        //         });
        //     }
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Put(Guid id, CreateAchievementDTO dto)
        // {
        //     try
        //     {

        //         var Achievement = await repoAchievement.FindById(id);

        //         if (Achievement == null)
        //         {
        //             return NotFound(new ApiResponse<Achievement>
        //             {
        //                 Message = "entity not found",
        //                 Data = null
        //             });
        //         }
                
        //         Achievement.Description = dto.Description;
        //         Achievement.Name = dto.Name;
        //         Achievement.Level = dto.Level;

        //         if (!ModelState.IsValid)
        //         {
        //             return Ok(new ApiResponse<Achievement>
        //             {
        //                 Data = null,
        //                 Message = "invalid params"
        //             });
        //         }

        //         return Ok(new ApiResponse<Achievement>
        //         {
        //             Message = "update success",
        //             Data = await repoAchievement.Update(Achievement),
        //         });
        //     }
        //     catch
        //     {
        //         return BadRequest(new ApiResponse<Achievement>
        //         {
        //             Message = "server error",
        //             Data = null,
        //         });
        //     }
        // }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Achievement> jsonPatch)
        {
            try
            {
                var Achievement = await repoAchievement.FindById(id);
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
                    Data = await repoAchievement.Update(Achievement),
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
                var result = await repoAchievement.Delete(id);

                if (!result) return Ok(new ApiResponse<Achievement>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<Achievement>
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
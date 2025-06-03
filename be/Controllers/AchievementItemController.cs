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
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementItemController(IRepository<AchievementItem> _repoAchievementItem) : ControllerBase
    {
        private readonly IRepository<AchievementItem> repoAchievementItem = _repoAchievementItem;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var existAchievementItem = await repoAchievementItem.FindById(id);

            if (existAchievementItem == null)
            {
                return NotFound(new ApiResponse<AchievementItem>
                {
                    Message = "entity not found",
                    Data = null
                });
            }

            return Ok(new ApiResponse<AchievementItem>
            {
                Message = "success",
                Data = existAchievementItem
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query)
        {
            return Ok(await repoAchievementItem.FindAll(query));
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] CreateAchievementItemDTO dto)
        // {
        //     try
        //     {
        //         var result = await repoAchievementItem.Create(new AchievementItem
        //         {
        //             Name = dto.Name,
        //             Description = dto.Description,
        //             Level = dto.Level
        //         });

        //         if (result == null)
        //         {
        //             return Ok(new ApiResponse<AchievementItem>
        //             {
        //                 Message = "create failed",
        //                 Data = null,
        //             });
        //         }

        //         return Ok(new ApiResponse<AchievementItem>
        //         {
        //             Message = "create success",
        //             Data = result,
        //         });
        //     }
        //     catch
        //     {
        //         return BadRequest(new ApiResponse<AchievementItem>
        //         {
        //             Message = "server error",
        //             Data = null,
        //         });
        //     }
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Put(Guid id, CreateAchievementItemDTO dto)
        // {
        //     try
        //     {

        //         var AchievementItem = await repoAchievementItem.FindById(id);

        //         if (AchievementItem == null)
        //         {
        //             return NotFound(new ApiResponse<AchievementItem>
        //             {
        //                 Message = "entity not found",
        //                 Data = null
        //             });
        //         }

        //         AchievementItem.Description = dto.Description;
        //         AchievementItem.Name = dto.Name;
        //         AchievementItem.Level = dto.Level;

        //         if (!ModelState.IsValid)
        //         {
        //             return Ok(new ApiResponse<AchievementItem>
        //             {
        //                 Data = null,
        //                 Message = "invalid params"
        //             });
        //         }

        //         return Ok(new ApiResponse<AchievementItem>
        //         {
        //             Message = "update success",
        //             Data = await repoAchievementItem.Update(AchievementItem),
        //         });
        //     }
        //     catch
        //     {
        //         return BadRequest(new ApiResponse<AchievementItem>
        //         {
        //             Message = "server error",
        //             Data = null,
        //         });
        //     }
        // }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<AchievementItem> jsonPatch)
        {
            try
            {
                var AchievementItem = await repoAchievementItem.FindById(id);
                if (AchievementItem == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<AchievementItem>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(AchievementItem, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<AchievementItem>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<AchievementItem>
                {
                    Message = "update success",
                    Data = await repoAchievementItem.Update(AchievementItem),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<AchievementItem>
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
                var result = await repoAchievementItem.Delete(id);

                if (!result) return Ok(new ApiResponse<AchievementItem>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<AchievementItem>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<AchievementItem>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
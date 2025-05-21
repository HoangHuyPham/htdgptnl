using be.DTOs.AchievementItem;
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
    public class AchievementItemController(IRepository<AchievementItem> _AchievementItemRepo) : ControllerBase
    {
        private readonly IRepository<AchievementItem> AchievementItemRepo = _AchievementItemRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var AchievementItems = await AchievementItemRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<AchievementItem>>
            {
                Message = "get success",
                Data = AchievementItems,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAchievementItemDTO dto)
        {
            try
            {
                var result = await AchievementItemRepo.Create(new AchievementItem
                {
                    Name = dto.Name,
                    AchievementId = dto.AchievementId,
                    Stretch = dto.Stretch,
                    Target = dto.Target,
                    Threshold = dto.Threshold,
                    Weight = dto.Weight
                });

                if (result == null)
                {
                    return Ok(new ApiResponse<AchievementItem>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<AchievementItem>
                {
                    Message = "create success",
                    Data = result,
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PutAchievementItemDTO dto)
        {
            try
            {

                var AchievementItem = await AchievementItemRepo.FindById(id);

                if (AchievementItem == null)
                {
                    return NotFound(new ApiResponse<AchievementItem>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }
                
                AchievementItem.Name = dto.Name;
                AchievementItem.Threshold = dto.Threshold;
                AchievementItem.Target = dto.Target;
                AchievementItem.Stretch = dto.Stretch;
                AchievementItem.Weight = dto.Weight;

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
                    Data = await AchievementItemRepo.Update(AchievementItem),
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<AchievementItem> jsonPatch)
        {
            try
            {
                var AchievementItem = await AchievementItemRepo.FindById(id);
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
                    Data = await AchievementItemRepo.Update(AchievementItem),
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
                var result = await AchievementItemRepo.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateAchievementItemDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateAchievementItemDTO>
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
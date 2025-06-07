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

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriteriaController(IRepository<Criteria> _repoCriteria) : ControllerBase
    {
        private readonly IRepository<Criteria> repoCriteria = _repoCriteria;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query)
        {
            return Ok(await repoCriteria.FindAll(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCriteriaDTO dto)
        {
            try
            {
                var result = await repoCriteria.Create(new Criteria
                {
                    Content = dto.Content,
                    EvidenceRequired = dto.EvidenceRequired,
                    AchievementItemId = dto.AchievementItemId
                });

                if (result == null)
                {
                    return Ok(new ApiResponse<Criteria>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<Criteria>
                {
                    Message = "create success",
                    Data = result,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Criteria>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CreateCriteriaDTO dto)
        {
            try
            {

                var Criteria = await repoCriteria.FindById(id);

                if (Criteria == null)
                {
                    return NotFound(new ApiResponse<Criteria>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                Criteria.Content = dto.Content;
                Criteria.EvidenceRequired = dto.EvidenceRequired;
                Criteria.AchievementItemId = dto.AchievementItemId;


                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<Criteria>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<Criteria>
                {
                    Message = "update success",
                    Data = await repoCriteria.Update(Criteria),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Criteria>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Criteria> jsonPatch)
        {
            try
            {
                var Criteria = await repoCriteria.FindById(id);
                if (Criteria == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<Criteria>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(Criteria, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<Criteria>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<Criteria>
                {
                    Message = "update success",
                    Data = await repoCriteria.Update(Criteria),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Criteria>
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
                var result = await repoCriteria.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateCriteriaDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateCriteriaDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Criteria>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
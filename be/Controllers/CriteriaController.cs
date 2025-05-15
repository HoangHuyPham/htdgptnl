using be.DTOs.Criteria;
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
    public class CriteriaController(IRepository<Criteria> _CriteriaRepo) : ControllerBase
    {
        private readonly IRepository<Criteria> CriteriaRepo = _CriteriaRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var Criterias = await CriteriaRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<Criteria>>
            {
                Message = "get success",
                Data = Criterias,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCriteriaDTO dto)
        {
            try
            {
                var result = await CriteriaRepo.Create(new Criteria
                {
                    Content = dto.Content,
                    ProofRequired = dto.ProofRequired,
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
        public async Task<IActionResult> Put(Guid id, PutCriteriaDTO dto)
        {
            try
            {

                var Criteria = await CriteriaRepo.FindById(id);

                if (Criteria == null)
                {
                    return NotFound(new ApiResponse<Criteria>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }
                
                Criteria.Content = dto.Content;
                Criteria.ProofRequired = dto.ProofRequired;

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
                    Data = await CriteriaRepo.Update(Criteria),
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
                var Criteria = await CriteriaRepo.FindById(id);
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
                    Data = await CriteriaRepo.Update(Criteria),
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
                var result = await CriteriaRepo.Delete(id);

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
using be.DTOs.ProofImage;
using be.Helpers;
using be.Mappers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProofImageController(IRepository<ProofImage> _ProofImageRepo) : ControllerBase
    {
        private readonly IRepository<ProofImage> ProofImageRepo = _ProofImageRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var ProofImages = await ProofImageRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<ProofImage>>
            {
                Message = "get success",
                Data = ProofImages,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProofImageDTO dto)
        {
            try
            {
                var result = await ProofImageRepo.Create(new ProofImage
                {
                    EvaluateScoreId = dto.EvaluateScoreId,
                    ImageId = dto.ImageId,
                });

                if (result == null)
                {
                    return Ok(new ApiResponse<ProofImage>
                    {
                        Message = "create failed",
                        Data = null,
                    });
                }

                return Ok(new ApiResponse<ProofImage>
                {
                    Message = "create success",
                    Data = result,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<ProofImage>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PutProofImageDTO dto)
        {
            try
            {

                var ProofImage = await ProofImageRepo.FindById(id);

                if (ProofImage == null)
                {
                    return NotFound(new ApiResponse<ProofImage>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }
                
                ProofImage.EvaluateScoreId = dto.EvaluateScoreId;
                ProofImage.ImageId = dto.ImageId;

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<ProofImage>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<ProofImage>
                {
                    Message = "update success",
                    Data = await ProofImageRepo.Update(ProofImage),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<ProofImage>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<ProofImage> jsonPatch)
        {
            try
            {
                var ProofImage = await ProofImageRepo.FindById(id);
                if (ProofImage == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<ProofImage>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(ProofImage, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<ProofImage>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<ProofImage>
                {
                    Message = "update success",
                    Data = await ProofImageRepo.Update(ProofImage),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<ProofImage>
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
                var result = await ProofImageRepo.Delete(id);

                if (!result) return Ok(new ApiResponse<CreateProofImageDTO>
                {
                    Message = "id not found",
                    Data = null,
                });

                return Ok(new ApiResponse<CreateProofImageDTO>
                {
                    Message = "delete success",
                    Data = null,
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<ProofImage>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }
    }
}
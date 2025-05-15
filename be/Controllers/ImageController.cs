using be.DTOs.Image;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using be.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController(IImageService imageService, IRepository<Image> imageRepo) : ControllerBase
    {
        private readonly IImageService _imageService = imageService;
        private readonly IRepository<Image> _imageRepo = imageRepo;

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            // Check type (.jpg, .png)
            List<string> acceptType = [".jpg", ".png"];

            if (!acceptType.Contains(Path.GetExtension(file.FileName)))
                return Ok(new ApiResponse<string>
                {
                    Message = "Only support .png, .jpg",
                    Data = null
                });

            // Check Size (5mb)
            if (file.Length > 1024 * 1024 * 5)
            {
                return Ok(new ApiResponse<string>
                {
                    Message = "File is exceed size limit (5mb).",
                    Data = null
                });
            }


            var result = await _imageService.Upload(file);
            if (result == null)
            {
                return Ok(new ApiResponse<string>
                {
                    Message = "upload failed.",
                    Data = null
                });
            }

            var image = await _imageRepo.Create(new Image
            {
                Size = file.Length,
                Url = "/static/" + result
            });

            if (image == null)
            {
                return Ok(new ApiResponse<string>
                {
                    Message = "create failed",
                    Data = null
                });
            }

            return Ok(new ApiResponse<Image>
            {
                Message = "create success",
                Data = image
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ImageCreateDTO createDTO)
        {
            var image = await _imageRepo.Create(new Image
            {
                Size = createDTO.Size ?? 0,
                Url = createDTO.Url
            });

            if (image == null)
            {
                return Ok(new ApiResponse<string>
                {
                    Message = "create failed.",
                    Data = null
                });
            }

            return Ok(new ApiResponse<Image>
            {
                Message = "create success",
                Data = image
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _imageRepo.Delete(id);

            if (!result) return Ok(new ApiResponse<ImageCreateDTO>
            {
                Message = "id not found",
                Data = null,
            });

            return Ok(new ApiResponse<ImageCreateDTO>
            {
                Message = "delete success",
                Data = null,
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Services.Interfaces
{
    public interface IImageService
    {
        Task<string?> Upload(IFormFile file);
    }
}
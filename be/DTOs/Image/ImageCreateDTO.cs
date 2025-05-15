using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace be.DTOs.Image
{
    public class ImageCreateDTO
    {
        [Required]
        public required string Url { get; set; }
        public long? Size { get; set; }
    }
}
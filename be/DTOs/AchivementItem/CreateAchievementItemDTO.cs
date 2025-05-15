using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.AchievementItem
{
    public class CreateAchievementItemDTO
    {
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public float Threshold { get; set; } = 0;
        public float Target { get; set; } = 0;
        public float Stretch { get; set; } = 0;
        public float Weight { get; set; } = 0;
        public Guid? AchievementId { get; set; }
    }
}
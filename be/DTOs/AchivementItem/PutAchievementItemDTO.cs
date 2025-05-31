using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.AchievementItem
{
    public class PutAchievementItemDTO
    {
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public float Weight { get; set; } = 0;
    }
}
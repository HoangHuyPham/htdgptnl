using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.Achievement
{
    public class CreateAchievementDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        public float? TotalWeight { get; set; } = 0;
        public Guid? PerformanceEvaluationId { get; set; }
    }
}
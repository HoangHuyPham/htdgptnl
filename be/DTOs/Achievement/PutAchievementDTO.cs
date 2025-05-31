using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.Achievement
{
    public class PutAchievementDTO
    {
        [Required]
        public string? Name { get; set; } = null!;
        public float Threshold { get; set; } = 0;
        public float Target { get; set; } = 0;
        public float Stretch { get; set; } = 0;
        public float? TotalWeight { get; set; } = 0;
        public Guid? PerformanceEvaluationId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class Achievement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public float Threshold { get; set; } = 80;
        public float Target { get; set; } = 100;
        public float Stretch { get; set; } = 120;
        public float? TotalWeight { get; set; } = 0;
        public ICollection<AchievementItem>? AchievementItems{ get; set; } = [];
        public Guid? PerformanceEvaluationId { get; set; }
        [JsonIgnore]
        public PerformanceEvaluation? PerformanceEvaluation { get; set; }
    }
}
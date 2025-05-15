using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class Achievement
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public float? TotalWeight { get; set; } = 0;
        public ICollection<AchievementItem>? AchivementItems{ get; set; } = [];
        public Guid? PerformanceEvaluationId { get; set; }
        [JsonIgnore]
        public PerformanceEvaluation? PerformanceEvaluation { get; set; }
    }
}
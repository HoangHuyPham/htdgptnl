using Newtonsoft.Json;

namespace be.Models
{
    public class Achievement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        public float Threshold { get; set; } = 0;
        public float Target { get; set; } = 0;
        public float Stretch { get; set; } = 0;
        public float Weight { get; set; } = 0;
        public Guid? PerformanceEvaluationId { get; set; }
        [JsonIgnore]
        public PerformanceEvaluation? PerformanceEvaluation { get; set; }
        public ICollection<AchievementItem> AchievementItems { get; set; } = [];
    }
}
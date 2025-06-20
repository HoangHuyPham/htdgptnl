using Newtonsoft.Json;

namespace be.Models
{
    public class AchievementItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        public float Threshold { get; set; } = 0;
        public float Target { get; set; } = 0;
        public float Stretch { get; set; } = 0;
        public float Weight { get; set; } = 0;

        public Guid? AchievementId { get; set; }
        [JsonIgnore]
        public Achievement? Achievement { get; set; }
        public ICollection<Criteria> Criterias { get; set; } = [];
    }
}
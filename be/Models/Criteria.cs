using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class Criteria
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = String.Empty;
        public bool EvidenceRequired { get; set; } = false;

        public Guid? AchievementItemId { get; set; }
        [JsonIgnore]
        public AchievementItem? AchievementItem { get; set; }

        [JsonIgnore]
        public ICollection<EvaluationScore> EvaluationScores { get; set; } = [];
    }
}
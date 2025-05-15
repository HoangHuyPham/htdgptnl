using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class Criteria
    {
        public Guid Id { get; set; }
        public string? Content { get; set; } = null!;
        public bool? ProofRequired { get; set; } = false;
        [JsonIgnore]
        public ICollection<EvaluateScore>? EvaluateScores { get; set; } = [];
        public Guid? AchievementItemId { get; set; }
        [JsonIgnore]
        public AchievementItem? AchievementItem { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class PerformanceEvaluation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public long Start { get; set; } = 0;
        public long End { get; set; } = 0;
        public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        [JsonIgnore]
        public ICollection<EvaluationSchedule> EvaluationSchedules { get; set; } = [];
        public ICollection<Achievement> Achievements { get; set; } = [];
    }
}
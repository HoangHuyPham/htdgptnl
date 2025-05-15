using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace be.Models
{
    public class EvaluationSchedule
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; } = null!;
        public string Status { get; set; } = "inactive";
        public ICollection<Role> Roles { get; set; } = [];
        public PerformanceEvaluation? PerformanceEvaluation { get; set; }
        [JsonIgnore]
        public ICollection<RoleEvaluationSchedule>? RoleEvaluationSchedules { get; set; } = [];
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class RoleType
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        [JsonIgnore]
        public ICollection<RoleSchedule>? RoleSchedules { get; set; } = [];
        [JsonIgnore]
        public ICollection<EvaluationScore>? EvaluationScores { get; set; } = [];
    }
}
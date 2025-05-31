using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class Schedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? Start { get; set; } = DateTime.UtcNow;
        public DateTime? End { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; } = string.Empty;

        [JsonIgnore]
        public EvaluationSchedule? EvaluationSchedule { get; set; }
        public Guid? EvaluationScheduleId { get; set; }
        
        [JsonIgnore]
        public RoleSchedule? RoleSchedule { get; set; }
        public Guid? RoleScheduleId { get; set; }
    }
}
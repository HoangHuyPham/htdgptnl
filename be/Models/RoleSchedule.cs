using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class RoleSchedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonIgnore]
        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }
        public Guid? ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }
        public Guid? RoleTypeId { get; set; }
        public RoleType? RoleType { get; set; }
        public Guid? EvaluationScheduleId { get; set; }
        public EvaluationSchedule? EvaluationSchedule { get; set; }
    }
}
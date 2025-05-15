using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace be.Models
{
    public class RoleEvaluationSchedule
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid EvaluationScheduleId { get; set; }
        public Role Role { get; set; } = null!;
        public EvaluationSchedule EvaluationSchedule { get; set; } = null!;
    }
}
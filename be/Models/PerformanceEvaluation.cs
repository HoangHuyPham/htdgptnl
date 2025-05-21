using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class PerformanceEvaluation
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public ICollection<Achievement> Achievements { get; set; } = [];
        public Guid? EvaluationScheduleId { get; set; }
        public EvaluationSchedule? EvaluationSchedule { get; set; }
    }
}
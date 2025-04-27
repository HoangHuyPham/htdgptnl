using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class PerformanceEvaluation
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Archievement> Archievements { get; set; } = [];
        public Guid? EvaluationScheduleId { get; set; }
        public EvaluationSchedule? EvaluationSchedule { get; set; }
    }
}
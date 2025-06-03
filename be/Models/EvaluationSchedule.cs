using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class EvaluationSchedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsSelfEvalution { get; set; } = false;
        public string Description { get; set; } = Guid.NewGuid().ToString();
        public long Start { get; set; } = 0;
        public long End { get; set; } = 0;
        public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        public Guid? PerformanceEvaluationId { get; set; }
        public PerformanceEvaluation? PerformanceEvaluation { get; set; }

        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
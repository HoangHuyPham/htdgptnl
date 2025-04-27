using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Archievement
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public float TotalWeight { get; set; } = 0;
        public ICollection<ArchievementItem>? ArchivementItems{ get; set; } = [];
        public Guid? PerformanceEvaluationId { get; set; }
        public PerformanceEvaluation? PerformanceEvaluation { get; set; }
    }
}
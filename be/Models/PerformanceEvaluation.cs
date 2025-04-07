using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class PerformanceEvaluation
    {
        public Guid Id { get; set; }
        public double TotalScoreWhat { get; set; } = 0;
        public double TotalScore { get; set; } = 0;
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<ArchivementCriteria>? ArchivementCriterias{ get; set; } = [];
    }
}
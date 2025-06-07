using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models.Enums;

namespace be.DTOs.EvaluationSchedule
{
    public class CreateEvaluationScheduleDTO
    {
        public string Description { get; set; } = Guid.NewGuid().ToString();
        public long Start { get; set; } = 0;
        public long End { get; set; } = 0;
        public Guid? PerformanceEvaluationId { get; set; }

        public Guid? RoleId { get; set; }
    }
}
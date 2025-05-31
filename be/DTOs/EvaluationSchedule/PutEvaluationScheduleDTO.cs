using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.EvaluationSchedule
{
    public class PutEvaluationScheduleDTO
    {
        public Guid? PerformanceEvaluationId { get; set; }
        public Guid? ScheduleId { get; set; }
    }
}
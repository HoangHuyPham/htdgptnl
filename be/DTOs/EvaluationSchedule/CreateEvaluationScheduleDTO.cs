using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.EvaluationSchedule
{
    public class CreateEvaluationScheduleDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
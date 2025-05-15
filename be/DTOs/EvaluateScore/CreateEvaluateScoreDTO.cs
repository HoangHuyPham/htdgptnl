using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace be.DTOs.EvaluateScore
{
    using be.Models;
    public class CreateEvaluateScoreDTO
    {
        public float? Score { get; set; } = 0;
        public Guid? EmployeeId { get; set; }
        public Guid? CriteriaId { get; set; }
    }
}
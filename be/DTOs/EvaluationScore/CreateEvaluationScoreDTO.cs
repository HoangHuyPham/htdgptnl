using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace be.DTOs.EvaluationScore
{
    using be.Models;
    public class CreateEvaluationScoreDTO
    {
        public float? Score { get; set; } = 0;
        public Guid? TargetId { get; set; }

        public Guid? CriteriaId { get; set; }
    }
}
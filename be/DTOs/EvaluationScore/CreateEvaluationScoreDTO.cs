using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models.Enums;

namespace be.DTOs.EvaluationScore
{
    public class CreateEvaluationScoreDTO
    {
        public float Score { get; set; } = 0;
        public string Comment { get; set; } = String.Empty;
        public Guid? TargetId { get; set; }

        public Guid? CriteriaId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace be.DTOs.EvaluationScore
{
    using be.Models;
    public class PutEvaluationScoreDTO
    {
        public float? Score { get; set; } = 0;
    }
}
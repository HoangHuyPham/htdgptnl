using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace be.DTOs.Criteria
{
    using be.Models;
    public class CreateCriteriaDTO
    {
        public string? Content { get; set; } = null!;
        public bool? ProofRequired { get; set; } = false;
        public Guid? AchievementItemId { get; set; }
    }
}
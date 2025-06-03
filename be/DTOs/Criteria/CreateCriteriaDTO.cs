using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models.Enums;

namespace be.DTOs.Criteria
{
    public class CreateCriteriaDTO
    {
        public string Content { get; set; } = String.Empty;
        public bool EvidenceRequired { get; set; } = false;
        public Guid? AchievementItemId { get; set; }
    }
}
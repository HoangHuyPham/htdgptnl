using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class ArchivementCriteria
    {
        public Guid Id { get; set; }
        public double TotalScore { get; set; } = 0;
        public Guid? ArchivementId { get; set; }
        public Archievement? Archievement{ get; set; }
        public Guid? CriteriaId { get; set; }
        public Criteria? Criteria{ get; set; }
    }
}
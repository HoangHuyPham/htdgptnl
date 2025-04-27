using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Criteria
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public bool ProofRequired { get; set; }
        public Guid? EvaluateScoreId { get; set; }
        public EvaluateScore? EvaluateScore { get; set; }
        public ICollection<ProofImage> ProofImages{ get; set; } = [];
        public Guid? ArchivementId { get; set; }
        public Archievement? Archievement { get; set; }
    }
}
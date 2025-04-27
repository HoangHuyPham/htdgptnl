using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class ArchievementItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public float Threshold { get; set; } = 0;
        public float Target { get; set; } = 0;
        public float Stretch { get; set; } = 0;
        public float Weight { get; set; } = 0;
        public Guid? ArchivementId { get; set; }
        public Archievement? Archievement{ get; set; }
        public ICollection<Criteria> Criterias{ get; set; } = [];
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class ProofImage
    {
        public Guid Id { get; set; }
        public Guid? ImageId { get; set; }
        public Image? Image { get; set; }
        public Guid? ProofCriteriaId { get; set; }
        public Criteria? ProofCriteria { get; set; }
    }
}
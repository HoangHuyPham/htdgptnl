using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.ProofImage
{
    public class PutProofImageDTO
    {
        public Guid? ImageId { get; set; }
        public Guid? EvaluateScoreId { get; set; }
    }
}
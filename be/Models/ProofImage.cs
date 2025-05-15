using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class ProofImage
    {
        public Guid Id { get; set; }
        public Guid? ImageId { get; set; }
        public Image? Image { get; set; }
        public Guid? EvaluateScoreId { get; set; }
        [JsonIgnore]
        public EvaluateScore? EvaluateScore { get; set; }
    }
}
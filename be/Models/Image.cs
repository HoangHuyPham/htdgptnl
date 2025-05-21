using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace be.Models
{
    public class Image
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string Url { get; set; } = null!;
        public long Size { get; set; }
        [JsonIgnore]
        public ProofImage? ProofImage { get; set; }
    }
}
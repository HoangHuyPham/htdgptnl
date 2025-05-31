using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class AchievementItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public float Weight { get; set; } = 0;
        public Guid? AchievementId { get; set; }
        [JsonIgnore]
        public Achievement? Achievement{ get; set; }
        public ICollection<Criteria>? Criterias{ get; set; } = [];
    }
}
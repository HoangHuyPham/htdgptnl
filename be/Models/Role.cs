using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models.Enums;
using Newtonsoft.Json;

namespace be.Models
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public RoleLevel Level { get; set; } = RoleLevel.Staff;
        [JsonIgnore]
        public ICollection<User> Users { get; set; } = [];
        [JsonIgnore]
        public ICollection<EvaluationSchedule> EvaluationSchedules { get; set; } = [];
    }
}
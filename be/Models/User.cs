using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; } = String.Empty;
        public string? Phone { get; set; } = String.Empty;
        public Employee? Employee { get; set; }
        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }

        [JsonIgnore]
        public ICollection<EvaluationScore> EvaluationScoreSources { get; set; } = [];

        [JsonIgnore]
        public ICollection<EvaluationScore> EvaluationScoreTargets { get; set; } = [];
    }
}
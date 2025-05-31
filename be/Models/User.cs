using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace be.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Username { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        [JsonIgnore]
        public ICollection<EvaluationScore> EvaluationScoreSources { get; set; } = [];
        [JsonIgnore]
        public ICollection<EvaluationScore> EvaluationScoreTargets { get; set; } = [];
    }
}
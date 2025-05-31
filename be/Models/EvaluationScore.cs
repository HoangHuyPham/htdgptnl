using Newtonsoft.Json;

namespace be.Models
{
    public class EvaluationScore
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public float? Score { get; set; } = 0;
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? SourceId { get; set; }
        [JsonIgnore]
        public User? Source { get; set; }
        public Guid? TargetId { get; set; }
        [JsonIgnore]
        public User? Target { get; set; }
        public Guid? CriteriaId { get; set; }
        [JsonIgnore]
        public Criteria? Criteria { get; set; }
        public Guid? SourceRoleTypeId { get; set; }
        [JsonIgnore]
        public RoleType? SourceRoleType { get; set; }
        public ICollection<ProofImage> ProofImages { get; set; } = [];
    }
}
namespace be.Models
{
    public class EvaluationScore
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public float Score { get; set; } = 0;
        public string Comment { get; set; } = String.Empty;
        public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        public Guid? SourceId { get; set; }
        public User? Source { get; set; }

        public Guid? TargetId { get; set; }
        public User? Target { get; set; }

        public Guid? CriteriaId { get; set; }
        public Criteria? Criteria { get; set; }
        
        public ICollection<Evidence> Evidences { get; set; } = [];
    }
}
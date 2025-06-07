namespace be.Models
{
    public class Evidence
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = String.Empty;
        public bool EvidenceRequired { get; set; } = false;
        public Image? Image { get; set; }
        public Guid? EvaluationScoreId { get; set; }
        public EvaluationScore? EvaluationScore{ get; set; }
    }
}
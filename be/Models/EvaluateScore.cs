namespace be.Models
{
    public class EvaluateScore
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public float? Score { get; set; } = 0;
        public Guid? EmployeeId { get; set; }
        public Employee? EmployeeEvaluate { get; set; }
        public Guid? CriteriaId { get; set; }
        public Criteria? Criteria { get; set; }
        public ICollection<ProofImage> ProofImages{ get; set; } = [];
    }
}
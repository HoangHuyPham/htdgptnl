namespace be.Models
{
    public class EvaluateScore
    {
        public Guid Id { get; set; }
        public float Score { get; set; } = 0;
        public Guid? EmployeeEvaluateId { get; set; }
        public Employee? EmployeeEvaluate { get; set; }
        public Criteria? Criteria { get; set; }
    }
}
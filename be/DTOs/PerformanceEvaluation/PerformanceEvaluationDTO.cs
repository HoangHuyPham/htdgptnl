using be.Models;

namespace be.DTOs.PerformanceEvaluation
{
    using be.Models;
    public class PerformanceEvaluationDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Name { get; set; }
        public ICollection<Achievement> Achievements { get; set; } = [];
        public Guid? EvaluationScheduleId { get; set; }
    }
}
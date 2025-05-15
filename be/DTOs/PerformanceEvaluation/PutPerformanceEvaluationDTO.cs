using be.Models;

namespace be.DTOs.PerformanceEvaluation
{
    public class PutPerformanceEvaluationDTO
    {
        public string? Name { get; set; } = Guid.NewGuid().ToString();
    }
}
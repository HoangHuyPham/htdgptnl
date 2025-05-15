using be.Models;

namespace be.DTOs.PerformanceEvaluation
{
    public class CreatePerformanceEvaluationDTO
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();
    }
}
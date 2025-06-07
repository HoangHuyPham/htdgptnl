using be.Models;
using be.Repos.Interfaces;

namespace be.Services.Interfaces
{
    public interface IEvaluationScheduleRepository : IRepository<EvaluationSchedule>
    {
        Task<List<EvaluationSchedule>> FindAllAvailable();
    }
}
using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class EvaluationScheduleRepository(ApplicationDbContext context) : IRepository<EvaluationSchedule>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<EvaluationSchedule?> Create(EvaluationSchedule target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existEvaluationSchedule = await _context.EvaluationSchedules.FirstOrDefaultAsync(x => x.Id == id);
            if (existEvaluationSchedule == null) return false;
            var result = _context.EvaluationSchedules.Remove(existEvaluationSchedule);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<EvaluationSchedule>> FindAll()
        {
            return await _context.EvaluationSchedules.ToListAsync();
        }

        public async Task<EvaluationSchedule?> FindById(Guid id)
        {
            var existEvaluationSchedule = await _context.EvaluationSchedules.FirstOrDefaultAsync(x => x.Id == id);
            if (existEvaluationSchedule == null) return null;
            return existEvaluationSchedule;
        }

        public async Task<EvaluationSchedule?> Update(EvaluationSchedule data)
        {
             _context.EvaluationSchedules.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
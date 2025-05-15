using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class PerformanceEvaluationRepository(ApplicationDbContext context) : IRepository<PerformanceEvaluation>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<PerformanceEvaluation?> Create(PerformanceEvaluation target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existPerformanceEvaluation = await _context.PerformanceEvaluations.FirstOrDefaultAsync(x => x.Id == id);
            if (existPerformanceEvaluation == null) return false;
            var result = _context.PerformanceEvaluations.Remove(existPerformanceEvaluation);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<PerformanceEvaluation>> FindAll()
        {
            return await _context.PerformanceEvaluations.Include(x => x.Achievements).ThenInclude(x => x.AchivementItems!).ThenInclude(x => x.Criterias!).ToListAsync();
        }

        public async Task<PerformanceEvaluation?> FindById(Guid id)
        {
            var existPerformanceEvaluation = await _context.PerformanceEvaluations.FirstOrDefaultAsync(x => x.Id == id);
            if (existPerformanceEvaluation == null) return null;
            return existPerformanceEvaluation;
        }

        public async Task<PerformanceEvaluation?> Update(PerformanceEvaluation data)
        {
             _context.PerformanceEvaluations.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
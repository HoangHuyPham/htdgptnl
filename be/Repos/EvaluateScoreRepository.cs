using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class EvaluateScoreRepository(ApplicationDbContext context) : IRepository<EvaluateScore>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<EvaluateScore?> Create(EvaluateScore target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existEvaluateScore = await _context.EvaluateScores.FirstOrDefaultAsync(x => x.Id == id);
            if (existEvaluateScore == null) return false;
            var result = _context.EvaluateScores.Remove(existEvaluateScore);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<EvaluateScore>> FindAll()
        {
            return await _context.EvaluateScores.ToListAsync();
        }

        public async Task<EvaluateScore?> FindById(Guid id)
        {
            var existEvaluateScore = await _context.EvaluateScores.FirstOrDefaultAsync(x => x.Id == id);
            if (existEvaluateScore == null) return null;
            return existEvaluateScore;
        }

        public async Task<EvaluateScore?> Update(EvaluateScore data)
        {
             _context.EvaluateScores.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
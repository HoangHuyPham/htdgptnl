using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class EvaluationScoreRepository(ApplicationDbContext context) : IEvaluationScoreRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<EvaluationScore?> Create(EvaluationScore target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existEvaluationScore = await _context.EvaluationScores.FirstOrDefaultAsync(x => x.Id == id);
            if (existEvaluationScore == null) return false;
            var result = _context.EvaluationScores.Remove(existEvaluationScore);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<EvaluationScore>> FindAll()
        {
            return await _context.EvaluationScores.Include(x=>x.Source).Include(x=>x.Target).Include(x=>x.SourceRoleType).Include(x=>x.Criteria).ToListAsync();
        }

        public async Task<List<EvaluationScore>> FindAllByQuery(IEvaluationScoreQuery query)
        {
            var EvaluationScoreQuery = _context.EvaluationScores.Include(x => x.Source).Include(x => x.Target).Include(x => x.SourceRoleType).Include(x => x.Criteria).AsQueryable();

            if (query.SourceId != null)
            {
                EvaluationScoreQuery.Where(x => x.SourceId == query.SourceId);
            }

            if (query.SourceId != null)
            {
                EvaluationScoreQuery.Where(x => x.SourceId == query.SourceId);
            }

            if (query.SourceId != null)
            {
                EvaluationScoreQuery.Where(x => x.SourceId == query.SourceId);
            }

            return await EvaluationScoreQuery.ToListAsync();
        }

        public async Task<EvaluationScore?> FindById(Guid id)
        {
            var existEvaluationScore = await _context.EvaluationScores.Include(x=>x.Source).Include(x=>x.Target).Include(x=>x.SourceRoleType).Include(x=>x.Criteria).FirstOrDefaultAsync(x => x.Id == id);
            if (existEvaluationScore == null) return null;
            return existEvaluationScore;
        }

        public async Task<EvaluationScore?> Update(EvaluationScore data)
        {
             _context.EvaluationScores.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
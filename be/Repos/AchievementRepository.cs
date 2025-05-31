using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class AchievementRepository(ApplicationDbContext context) : IRepository<Achievement>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<Achievement?> Create(Achievement target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existAchievement = await _context.Achievements.FirstOrDefaultAsync(x => x.Id == id);
            if (existAchievement == null) return false;
            var result = _context.Achievements.Remove(existAchievement);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<Achievement>> FindAll()
        {
            return await _context.Achievements.Include(x=>x.AchievementItems!).ThenInclude(x=>x.Criterias).ToListAsync();
        }

        public async Task<Achievement?> FindById(Guid id)
        {
            var existAchievement = await _context.Achievements.FirstOrDefaultAsync(x => x.Id == id);
            if (existAchievement == null) return null;
            return existAchievement;
        }

        public async Task<Achievement?> Update(Achievement data)
        {
             _context.Achievements.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
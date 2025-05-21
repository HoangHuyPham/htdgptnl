using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class AchievementItemRepository(ApplicationDbContext context) : IRepository<AchievementItem>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<AchievementItem?> Create(AchievementItem target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existAchievementItem = await _context.AchievementItems.FirstOrDefaultAsync(x => x.Id == id);
            if (existAchievementItem == null) return false;
            var result = _context.AchievementItems.Remove(existAchievementItem);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<AchievementItem>> FindAll()
        {
            return await _context.AchievementItems.Include(x=>x.Criterias).ToListAsync();
        }

        public async Task<AchievementItem?> FindById(Guid id)
        {
            var existAchievementItem = await _context.AchievementItems.FirstOrDefaultAsync(x => x.Id == id);
            if (existAchievementItem == null) return null;
            return existAchievementItem;
        }

        public async Task<AchievementItem?> Update(AchievementItem data)
        {
             _context.AchievementItems.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
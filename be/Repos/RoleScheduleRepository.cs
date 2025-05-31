using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class RoleScheduleRepository(ApplicationDbContext context) : IRoleScheduleRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<RoleSchedule?> Create(RoleSchedule target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existRoleSchedule = await _context.RoleSchedules.FirstOrDefaultAsync(x => x.Id == id);
            if (existRoleSchedule == null) return false;
            var result = _context.RoleSchedules.Remove(existRoleSchedule);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<RoleSchedule>> FindAll()
        {
            return await _context.RoleSchedules
            .Include(x => x.Role)
            .Include(x => x.Schedule)
            .Include(x => x.RoleType)
            .Include(x => x.EvaluationSchedule)
            .ThenInclude(x => x!.PerformanceEvaluation)
            .ThenInclude(x => x!.Achievements)
            .ThenInclude(x => x!.AchievementItems)!
            .ThenInclude(x => x!.Criterias)
            .Include(x => x.EvaluationSchedule)
            .ThenInclude(x => x!.Schedule)
            .ToListAsync();
        }

        public async Task<List<RoleSchedule>> FindAllByRoleId(Guid id)
        {
            return await _context.RoleSchedules
            .Include(x => x.Role)
            .Include(x => x.Schedule)
            .Include(x => x.RoleType)
            .Include(x => x.EvaluationSchedule)
            .ThenInclude(x => x!.PerformanceEvaluation)
            .ThenInclude(x => x!.Achievements)
            .ThenInclude(x => x!.AchievementItems)!
            .ThenInclude(x => x!.Criterias)
            .Include(x => x.EvaluationSchedule)
            .ThenInclude(x => x!.Schedule)
            .Where(x => x.RoleId == id)
            .Where(x=>x.Schedule!.End > DateTime.UtcNow)
            .ToListAsync();
        }

        public async Task<RoleSchedule?> FindById(Guid id)
        {
            var existRoleSchedule = await _context.RoleSchedules.FirstOrDefaultAsync(x => x.Id == id);
            if (existRoleSchedule == null) return null;
            return existRoleSchedule;
        }

        public async Task<RoleSchedule?> Update(RoleSchedule data)
        {
            _context.RoleSchedules.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
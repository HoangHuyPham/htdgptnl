// using be.Contexts;
// using be.Models;
// using be.Repos.Interfaces;
// using Microsoft.EntityFrameworkCore;

// namespace be.Repos
// {
//     public class RoleEvaluationScheduleRepository(ApplicationDbContext context) : IRoleEvaluationScheduleRepository
//     {
//         private readonly ApplicationDbContext _context = context;
//         public async Task<RoleEvaluationSchedule?> Create(RoleEvaluationSchedule target)
//         {
//             await _context.AddAsync(target);
//             await _context.SaveChangesAsync();
//             return target;
//         }

//         public async Task<bool> Delete(Guid id)
//         {
//             var existRoleEvaluationSchedule = await _context.RoleEvaluationSchedules.FirstOrDefaultAsync(x => x.Id == id);
//             if (existRoleEvaluationSchedule == null) return false;
//             var result = _context.RoleEvaluationSchedules.Remove(existRoleEvaluationSchedule);
//             await _context.SaveChangesAsync();
//             return result != null;
//         }

//         public async Task<List<RoleEvaluationSchedule>> FindAll()
//         {
//             return await _context.RoleEvaluationSchedules.ToListAsync();
//         }

//         public async Task<List<RoleEvaluationSchedule>> FindAllByEvaluationId(Guid id)
//         {
//             return await _context.RoleEvaluationSchedules.Include(x=>x.EvaluationSchedule).ThenInclude(x=>x.PerformanceEvaluation!).ThenInclude(x=>x.Achievements).ThenInclude(x=>x.AchivementItems)!.ThenInclude(x=>x.Criterias).Where(x=>x.EvaluationScheduleId == id).OrderByDescending(x=>x.EvaluationSchedule.Start).ToListAsync();
//         }

//         public async Task<List<RoleEvaluationSchedule>> FindAllByRoleId(Guid id)
//         {
//             return await _context.RoleEvaluationSchedules.Include(x=>x.EvaluationSchedule).ThenInclude(x=>x.PerformanceEvaluation!).ThenInclude(x=>x.Achievements).ThenInclude(x=>x.AchivementItems)!.ThenInclude(x=>x.Criterias).Where(x=>x.RoleId == id).OrderByDescending(x=>x.EvaluationSchedule.Start).ToListAsync();
//         }

//         public async Task<RoleEvaluationSchedule?> FindById(Guid id)
//         {
//             var existRoleEvaluationSchedule = await _context.RoleEvaluationSchedules.FirstOrDefaultAsync(x => x.Id == id);
//             if (existRoleEvaluationSchedule == null) return null;
//             return existRoleEvaluationSchedule;
//         }

//         public async Task<RoleEvaluationSchedule?> Update(RoleEvaluationSchedule data)
//         {
//              _context.RoleEvaluationSchedules.Update(data);
//             await _context.SaveChangesAsync();
//             return data;
//         }

//     }
// }
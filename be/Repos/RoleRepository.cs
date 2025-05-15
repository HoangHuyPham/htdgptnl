using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class RoleRepository(ApplicationDbContext context) : IRepository<Role>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<Role?> Create(Role target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (existRole == null) return false;
            var result = _context.Roles.Remove(existRole);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<Role>> FindAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> FindById(Guid id)
        {
            var existRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (existRole == null) return null;
            return existRole;
        }

        public async Task<Role?> Update(Role data)
        {
             _context.Roles.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
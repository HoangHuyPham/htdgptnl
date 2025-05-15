using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<User?> Create(User target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existUser == null) return false;
            var result = _context.Users.Remove(existUser);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<User>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> FindById(Guid id)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existUser == null) return null;
            return existUser;
        }

        public async Task<User?> FindByUserName(string username)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (existUser == null) return null;
            return existUser;
        }

        public async Task<User?> Update(User data)
        {
             _context.Users.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
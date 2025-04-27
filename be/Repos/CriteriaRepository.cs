using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class CriteriaRepository(ApplicationDbContext context) : IRepository<Criteria>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<Criteria?> Create(Criteria target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<Criteria?> Delete(Guid id)
        {
            var existCriteria = await _context.Criterias.FirstOrDefaultAsync(x => x.Id == id);
            if (existCriteria == null) return null;
            _context.Criterias.Remove(existCriteria);
            await _context.SaveChangesAsync();
            return existCriteria;
        }

        public async Task<List<Criteria>> FindAll()
        {
            return await _context.Criterias.ToListAsync();
        }

        public async Task<Criteria?> FindById(Guid id)
        {
            var existCriteria = await _context.Criterias.FirstOrDefaultAsync(x => x.Id == id);
            if (existCriteria == null) return null;
            return existCriteria;
        }

        public async Task<Criteria?> Update(Guid id, Criteria data)
        {
             _context.Criterias.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
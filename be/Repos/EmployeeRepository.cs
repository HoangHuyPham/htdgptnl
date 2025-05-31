using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class EmployeeRepository(ApplicationDbContext context) : IRepository<Employee>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<Employee?> Create(Employee target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existEmployee == null) return false;
            var result = _context.Employees.Remove(existEmployee);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<Employee>> FindAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> FindById(Guid id)
        {
            var existEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existEmployee == null) return null;
            return existEmployee;
        }

        public async Task<Employee?> Update(Employee data)
        {
             _context.Employees.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
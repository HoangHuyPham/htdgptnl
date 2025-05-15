using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class ImageRepository(ApplicationDbContext context) : IRepository<Image>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<Image?> Create(Image target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existImage = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (existImage == null) return false;
            var result = _context.Images.Remove(existImage);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<Image>> FindAll()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Image?> FindById(Guid id)
        {
            var existImage = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (existImage == null) return null;
            return existImage;
        }

        public async Task<Image?> Update(Image data)
        {
             _context.Images.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
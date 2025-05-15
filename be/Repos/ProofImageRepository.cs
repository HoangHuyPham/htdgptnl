using be.Contexts;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class ProofImageRepository(ApplicationDbContext context) : IRepository<ProofImage>
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<ProofImage?> Create(ProofImage target)
        {
            await _context.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existProofImage = await _context.ProofImages.FirstOrDefaultAsync(x => x.Id == id);
            if (existProofImage == null) return false;
            var result = _context.ProofImages.Remove(existProofImage);
            await _context.SaveChangesAsync();
            return result != null;
        }

        public async Task<List<ProofImage>> FindAll()
        {
            return await _context.ProofImages.Include(x=>x.Image).ToListAsync();
        }

        public async Task<ProofImage?> FindById(Guid id)
        {
            var existProofImage = await _context.ProofImages.FirstOrDefaultAsync(x => x.Id == id);
            if (existProofImage == null) return null;
            return existProofImage;
        }

        public async Task<ProofImage?> Update(ProofImage data)
        {
             _context.ProofImages.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

    }
}
using CatAPI.Data;
using CatAPI.Interfaces;
using CatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatAPI.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly AppDbContext _context;

        public CatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveCatsAsync(IEnumerable<CatEntity> cats)
        {
            var newCats = new List<CatEntity>();
            
            foreach(var cat in cats)
            {
                var exists = await _context.Cats.AnyAsync(c => c.CatId == cat.CatId);
                if (!exists)
                {
                    newCats.Add(cat);
                }
            }            
            
            if (newCats.Any())
            {
                _context.Cats.AddRange(newCats);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CatEntity> GetCatByIdAsync(int id)
        {
            return await _context.Cats.FindAsync(id);
        }

        public async Task<IEnumerable<CatEntity>> GetCatsAsync(int page, int pageSize, string tag = null)
        {
            var query = _context.Cats.AsQueryable();

            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(c => c.CatTags.Any(ct => ct.Tag.Name == tag));
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}

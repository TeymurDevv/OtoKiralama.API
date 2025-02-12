using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly AppDbContext _context;
        public BrandRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Brand>().CountAsync();
        }

    }
}

using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class GearRepository : Repository<Gear>, IGearRepository
    {
        private readonly AppDbContext _context;
        public GearRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<Gear>().CountAsync();
        }
    }
}

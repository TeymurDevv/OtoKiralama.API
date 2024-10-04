using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class BodyRepository:Repository<Body>,IBodyRepository
    {
        private readonly AppDbContext _context;
        public BodyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Body>().CountAsync();
        }
    }
}

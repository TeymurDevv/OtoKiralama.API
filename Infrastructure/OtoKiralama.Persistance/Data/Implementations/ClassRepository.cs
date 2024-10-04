using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class ClassRepository : Repository<Class>, IClassRepository
    {
        private readonly AppDbContext _context;
        public ClassRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<Class>().CountAsync();
        }
    }
}

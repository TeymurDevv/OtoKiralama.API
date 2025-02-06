using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly AppDbContext _context;
        public LocationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<Location>().CountAsync();
        }

        public Task<int> CountAsync(Expression<Func<Location, bool>> predicate)
        {
            return _context.Set<Location>().CountAsync(predicate);
        }
    }
}

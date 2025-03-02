using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;
namespace OtoKiralama.Persistance.Data.Implementations
{
    internal class FilterRangeRepository : Repository<FilterRange>, IFilterRangeRepository
    {
        private readonly AppDbContext _context;
        public FilterRangeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<FilterRange>().CountAsync();
        }

    }
}

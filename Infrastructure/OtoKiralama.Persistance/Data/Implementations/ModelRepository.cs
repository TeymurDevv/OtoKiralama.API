using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        private readonly AppDbContext _context;
        public ModelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<Model>().CountAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class FuelRepository:Repository<Fuel>,IFuelRepository
    {
        private readonly AppDbContext _context;
        public FuelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<Fuel>().CountAsync();
        }
    }
}

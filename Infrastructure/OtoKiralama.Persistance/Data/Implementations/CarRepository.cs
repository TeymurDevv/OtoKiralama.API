using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Car>().CountAsync();
        }
    }
}

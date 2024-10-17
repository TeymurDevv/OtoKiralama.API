using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class CarPhotoRepository : Repository<CarPhoto>, ICarPhotoRepository
    {
        private readonly AppDbContext _context;
        public CarPhotoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<CarPhoto>().CountAsync();
        }
    }

}

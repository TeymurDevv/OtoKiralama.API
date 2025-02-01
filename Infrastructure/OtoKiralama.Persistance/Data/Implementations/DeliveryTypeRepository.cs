using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class DeliveryTypeRepository : Repository<DeliveryType>, IDeliveryTypeRepository
    {
        private readonly AppDbContext _context;
        public DeliveryTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<DeliveryType>().CountAsync();
        }
    }
}

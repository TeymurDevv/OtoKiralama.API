using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class GearRepository : Repository<Gear>, IGearRepository
    {
        public GearRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class FuelRepository:Repository<Fuel>,IFuelRepository
    {
        public FuelRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class BodyRepository:Repository<Body>,IBodyRepository
    {
        public BodyRepository(AppDbContext context) : base(context)
        {
        }
    }
}

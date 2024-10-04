using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface ILocationRepository:IRepository<Location>
    {
        Task<int> CountAsync();
    }
}

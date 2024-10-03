using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface ICarRepository: IRepository<Car>
    {
        Task<int> CountAsync();
    }
}

using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface ICarPhotoRepository: IRepository<CarPhoto>
    {
        Task<int> CountAsync();
    }
}

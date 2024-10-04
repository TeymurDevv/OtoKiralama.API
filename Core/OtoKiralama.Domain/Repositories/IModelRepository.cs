using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface IModelRepository:IRepository<Model>
    {
        Task<int> CountAsync();
    }
}

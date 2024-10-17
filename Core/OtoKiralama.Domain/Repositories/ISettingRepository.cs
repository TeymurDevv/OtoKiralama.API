using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface ISettingRepository : IRepository<Setting>
    {
        Task<int> CountAsync();
    }
}

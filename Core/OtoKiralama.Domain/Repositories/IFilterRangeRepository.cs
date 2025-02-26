using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface IFilterRangeRepository : IRepository<FilterRange>
    {
        Task<int> CountAsync();
    }
}

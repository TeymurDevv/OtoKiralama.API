using System.Linq.Expressions;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface ILocationRepository:IRepository<Location>
    {
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<Location, bool>> predicate);
    }
}

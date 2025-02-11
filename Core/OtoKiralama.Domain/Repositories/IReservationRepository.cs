using System.Linq.Expressions;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<Reservation, bool>> predicate);
        Task<int> GetLastReservationNumberForYear(int year);
    }
}

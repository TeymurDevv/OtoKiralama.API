using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface IReservationRepository:IRepository<Reservation>
    {
        Task<int> CountAsync();
    }
}

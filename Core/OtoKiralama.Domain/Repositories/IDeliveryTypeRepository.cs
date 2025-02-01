using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface IDeliveryTypeRepository : IRepository<DeliveryType>
    {
        Task<int> CountAsync();

    }
}

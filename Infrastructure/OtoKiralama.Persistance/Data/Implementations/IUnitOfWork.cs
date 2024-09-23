using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public interface IUnitOfWork
    {
        public ILocationRepository LocationRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public IGearRepository GearRepository { get; }
        public void Commit();

    }
}

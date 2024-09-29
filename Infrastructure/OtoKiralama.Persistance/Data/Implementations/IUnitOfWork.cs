using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public interface IUnitOfWork
    {
        public ILocationRepository LocationRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public IGearRepository GearRepository { get; }
        public IBodyRepository BodyRepository { get; }
        public IFuelRepository FuelRepository { get; set; }
        public void Commit();

    }
}

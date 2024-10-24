using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public interface IUnitOfWork
    {
        public ILocationRepository LocationRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public IGearRepository GearRepository { get; }
        public IBodyRepository BodyRepository { get; }
        public IFuelRepository FuelRepository { get; }
        public IClassRepository ClassRepository { get;}
        public ICarRepository CarRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
        public IModelRepository ModelRepository { get; }
        public ICarPhotoRepository CarPhotoRepository { get; }
        public ISettingRepository SettingRepository { get; }
        public IReservationRepository ReservationRepository { get; }
        public void Commit();

    }
}

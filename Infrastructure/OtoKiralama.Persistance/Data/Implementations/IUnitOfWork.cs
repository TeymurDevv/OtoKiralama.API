using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public interface IUnitOfWork : IDisposable
    {
        ILocationRepository LocationRepository { get; }
        IBrandRepository BrandRepository { get; }
        IGearRepository GearRepository { get; }
        IBodyRepository BodyRepository { get; }
        IFuelRepository FuelRepository { get; }
        IClassRepository ClassRepository { get; }
        ICarRepository CarRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IModelRepository ModelRepository { get; }
        ICarPhotoRepository CarPhotoRepository { get; }
        ISettingRepository SettingRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IDeliveryTypeRepository DeliveryTypeRepository { get; }

        // Tranzaksiya metodları
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        // Əməliyyatları yadda saxlamaq üçün metodlar
        Task CommitAsync();
    }
}

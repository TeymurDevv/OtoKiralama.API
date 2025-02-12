namespace OtoKiralama.Domain.Repositories
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
        IIndividualInvoiceRepository IndividualInvoiceRepository { get; }
        IIndividualCompanyInvoiceRepository IndividualCompanyInvoiceRepository { get; }
        ICorporateInvoiceRepository CorporateInvoiceRepository { get; }
        ICountryRepository CountryRepository { get; }
        


        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

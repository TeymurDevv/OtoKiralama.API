using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private bool _disposed;

        public UnitOfWork(AppDbContext context,
            ILocationRepository locationRepository,
            IBrandRepository brandRepository,
            IGearRepository gearRepository,
            IBodyRepository bodyRepository,
            IFuelRepository fuelRepository,
            IClassRepository classRepository,
            ICarRepository carRepository,
            ICompanyRepository companyRepository,
            IModelRepository modelRepository,
            ICarPhotoRepository carPhotoRepository,
            ISettingRepository settingRepository,
            IReservationRepository reservationRepository,
            IDeliveryTypeRepository deliveryTypeRepository,
            IIndividualInvoiceRepository individualInvoiceRepository,
            IIndividualCompanyInvoiceRepository individualCompanyInvoiceRepository,
            ICorporateInvoiceRepository corporateInvoiceRepository,
            ICountryRepository countryRepository
            
            )
        {
            _context = context;
            LocationRepository = locationRepository;
            BrandRepository = brandRepository;
            GearRepository = gearRepository;
            BodyRepository = bodyRepository;
            FuelRepository = fuelRepository;
            ClassRepository = classRepository;
            CarRepository = carRepository;
            CompanyRepository = companyRepository;
            ModelRepository = modelRepository;
            CarPhotoRepository = carPhotoRepository;
            SettingRepository = settingRepository;
            ReservationRepository = reservationRepository;
            DeliveryTypeRepository = deliveryTypeRepository;
            IndividualInvoiceRepository = individualInvoiceRepository;
            IndividualCompanyInvoiceRepository = individualCompanyInvoiceRepository;
            CorporateInvoiceRepository = corporateInvoiceRepository;
            CountryRepository = countryRepository;
        }

        public ILocationRepository LocationRepository { get; private set; }
        public IBrandRepository BrandRepository { get; private set; }
        public IGearRepository GearRepository { get; private set; }
        public IBodyRepository BodyRepository { get; private set; }
        public IFuelRepository FuelRepository { get; private set; }
        public IClassRepository ClassRepository { get; private set; }
        public ICarRepository CarRepository { get; private set; }
        public ICompanyRepository CompanyRepository { get; private set; }
        public IModelRepository ModelRepository { get; private set; }
        public ICarPhotoRepository CarPhotoRepository { get; private set; }
        public ISettingRepository SettingRepository { get; private set; }
        public IReservationRepository ReservationRepository { get; private set; }
        public IDeliveryTypeRepository DeliveryTypeRepository { get; private set; }
        public IIndividualInvoiceRepository IndividualInvoiceRepository { get; private set; }
        public IIndividualCompanyInvoiceRepository IndividualCompanyInvoiceRepository { get; private set; }
        public ICorporateInvoiceRepository CorporateInvoiceRepository { get; private set; }
        public ICountryRepository CountryRepository { get; private set; }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction == null)
            {
                await _context.Database.BeginTransactionAsync(cancellationToken);
            }
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}

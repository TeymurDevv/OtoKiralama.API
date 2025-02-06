using Microsoft.EntityFrameworkCore.Storage;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;

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
            IDeliveryTypeRepository deliveryTypeRepository)
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

        // Tranzaksiyanı başlatmaq
        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        // Əməliyyatları təsdiqləmək
        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        // Əməliyyatı geri qaytarmaq (rollback etmək)
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

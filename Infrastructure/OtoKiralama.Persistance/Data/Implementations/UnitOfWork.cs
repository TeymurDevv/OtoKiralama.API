using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, ILocationRepository locationRepository, IBrandRepository brandRepository, IGearRepository gearRepository, IBodyRepository bodyRepository, IFuelRepository fuelRepository, IClassRepository classRepository, ICarRepository carRepository, ICompanyRepository companyRepository, IModelRepository modelRepository, ICarPhotoRepository carPhotoRepository, ISettingRepository settingRepository)
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


        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

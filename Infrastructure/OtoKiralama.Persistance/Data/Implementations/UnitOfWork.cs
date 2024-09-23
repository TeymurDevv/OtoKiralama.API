using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ILocationRepository LocationRepository { get; private set; }
        public IBrandRepository BrandRepository { get; private set; }
        public IGearRepository GearRepository { get; private set; }
        public UnitOfWork(AppDbContext context, ILocationRepository locationRepository, IBrandRepository brandRepository, IGearRepository gearRepository)
        {
            _context = context;
            LocationRepository = locationRepository;
            BrandRepository = brandRepository;
            GearRepository = gearRepository;
        }

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

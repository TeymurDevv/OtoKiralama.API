using OtoKiralama.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ILocationRepository LocationRepository { get; private set; }
        public IBrandRepository BrandRepository { get; private set; }
        public UnitOfWork(AppDbContext context, ILocationRepository locationRepository, IBrandRepository brandRepository)
        {
            _context = context;
            LocationRepository = locationRepository;
            BrandRepository = brandRepository;
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

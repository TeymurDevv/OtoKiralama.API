using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AppDbContext context) : base(context)
        {
        }
    }
}

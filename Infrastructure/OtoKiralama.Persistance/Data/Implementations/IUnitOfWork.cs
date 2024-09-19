using OtoKiralama.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public interface IUnitOfWork
    {
        public ILocationRepository LocationRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public void Commit();

    }
}

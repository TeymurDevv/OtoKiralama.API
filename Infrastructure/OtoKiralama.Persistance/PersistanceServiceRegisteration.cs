using Microsoft.Extensions.DependencyInjection;
using OtoKiralama.Domain.Repositories;
using OtoKiralama.Persistance.Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Persistance
{
    public static class PersistanceServiceRegisteration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IGearRepository, GearRepository>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using OtoKiralama.Domain.Repositories;
using OtoKiralama.Persistance.Data.Implementations;

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
            services.AddScoped<IBodyRepository, BodyRepository>();
            services.AddScoped<IFuelRepository, FuelRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<ICarPhotoRepository, CarPhotoRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IDeliveryTypeRepository, DeliveryTypeRepository>();
        }
    }
}

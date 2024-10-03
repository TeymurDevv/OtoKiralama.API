using Microsoft.Extensions.DependencyInjection;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Infrastructure.Concretes;

namespace OtoKiralama.Infrastructure
{
    public static class InfrastructureServiceRegisteration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPhotoService, PhotoService>();
        }
    }
}

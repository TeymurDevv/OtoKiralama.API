using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Application.Settings;
using OtoKiralama.Infrastructure.Concretes;

namespace OtoKiralama.Infrastructure
{
    public static class InfrastructureServiceRegisteration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            services.AddScoped<IPhotoService, PhotoService>();
        }
    }
}

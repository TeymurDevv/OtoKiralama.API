using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Application.Services;
using OtoKiralama.Application.Validators.BrandValidator;

namespace OtoKiralama.Application
{
    public static class ApplicationServiceRegisteration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IGearService, GearService>();
            services.AddScoped<IBodyService, BodyService>();
            services.AddScoped<IFuelService, FuelService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarPhotoService, CarPhotoService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<BrandCreateValidator>();
        }
    }
}

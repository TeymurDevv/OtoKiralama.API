using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Application.Services;
using OtoKiralama.Application.Validators.BrandValidator;
using Stripe;
using ZiggyCreatures.Caching.Fusion;
using InvoiceService = OtoKiralama.Application.Services.InvoiceService;
using ProductService = Stripe.Climate.ProductService;
using TokenService = OtoKiralama.Application.Services.TokenService;

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
            services.AddScoped<IDeliveryTypeService, DeliveryTypeService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IFilterRangeService, FilterRangeService>();
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<BrandCreateValidator>();
            services.AddMemoryCache();

            services.AddFusionCache()
                .WithDefaultEntryOptions(new FusionCacheEntryOptions
                {
                    Duration = TimeSpan.FromMinutes(2),
                    Priority = CacheItemPriority.High,

                });
            services.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = true;
                opt.Providers.Add<BrotliCompressionProvider>();
                opt.Providers.Add<GzipCompressionProvider>();
            });
            services.AddScoped<ProductService>();
            services.AddScoped<ChargeService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<Stripe.Issuing.TokenService>();
        }
    }
}

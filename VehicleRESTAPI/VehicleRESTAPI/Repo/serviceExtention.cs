using VehicleRESTAPI.Repo.Interface;
using VehicleRESTAPI.Repo.Vehicle.Interface;
using VehicleRESTAPI.Repo.Vehicle;

namespace VehicleRESTAPI.Repo
{
    public static class serviceExtention
    {
        public static IServiceCollection interfaceInjection(this IServiceCollection services) {
            services.AddScoped<IUsers, usersRepo>();
            services.AddScoped<Ijwt, jwtRepo>();
            services.AddScoped<IvBrand, vBrandRepo>();
            services.AddScoped<IvType, vTypeRepo>();
            services.AddScoped<IvModel, vModelRepo>();
            services.AddScoped<IvYear, vYearRepo>();
            services.AddScoped<IvPricelist, vPricelistRepo>();
            return services;
        }
    }
}

using Quixa.Core.Providers;
using Quixa.Core.Repositories;
using Quixa.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Quixa.Core.Registrations
{
    public static class CoreRegistrations
    {
        public static IServiceCollection AddCoreComponents(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICarService, CarService>();
            services.AddSingleton<VersionProvider>();

            return services;
        }
    }
}

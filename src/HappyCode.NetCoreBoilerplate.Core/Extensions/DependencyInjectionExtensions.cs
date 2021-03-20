using Quixa.Core.Repositories;
using Quixa.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Quixa.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreComponents(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICarService, CarService>();

            return services;
        }
    }
}

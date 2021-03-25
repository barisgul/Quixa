using Quixa.Core.Repositories;
using Quixa.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Quixa.Core.Interfaces;
using Quixa.Core.RestApiHandler;

namespace Quixa.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreComponents(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IRestClientHandler, RestClientHandler>();
            services.AddScoped<IApiService, RegisteredApiService>();

            return services;
        }
    }
}

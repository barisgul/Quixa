using System.IO;
using Quixa.Api.Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Quixa.Api.Infrastructure.Registrations
{
    public static class SwaggerRegistration
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            string secretKey = configuration.GetValue<string>("ApiKey:SecretKey");

            services.AddSwaggerGen(swaggerOptions =>
            {

                swaggerOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Quixa Web Api",
                    Version = "v1",
                    Description = $"ApiKey {secretKey}",
                    Contact = new OpenApiContact
                    {
                        Name = "Łukasz Kurzyniec",
                        Url = new Uri("https://kurzyniec.pl/"),
                    }
                });

                swaggerOptions.OrderActionsBy(x => x.RelativePath);
                swaggerOptions.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quixa.Api.xml"));

                swaggerOptions.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "ApiKey needed to access the endpoints (eg: `Authorization: ApiKey xxx-xxx`)",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });

                swaggerOptions.OperationFilter<SecurityRequirementSwaggerOperationFilter>();
                swaggerOptions.DocumentFilter<FeatureFlagSwaggerDocumentFilter>();
            });
        }
    }
}

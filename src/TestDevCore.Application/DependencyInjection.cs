using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestDevCore.Application.Services.ExchangeRates;
using TestDevCore.Application.Shared;

namespace TestDevCore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

            services.AddScoped<ExchangeRateService, ExchangeRateService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}

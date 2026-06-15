using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestDevCore.Core.Interfaces;
using TestDevCore.Domain.Accounts.Repositories;
using TestDevCore.Domain.Currencies.Repositories;
using TestDevCore.Domain.ExchangeRates.Repositories;
using TestDevCore.Domain.Movements.Repositories;
using TestDevCore.Domain.Services.Movements.Operations;
using TestDevCore.Infrastructure.Persistence;
using TestDevCore.Infrastructure.Persistence.DomainModel;
using TestDevCore.Infrastructure.Persistence.PersistenceModel;
using TestDevCore.Infrastructure.Persistence.Repositories;

namespace TestDevCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PersistenceDBContext>(context =>
                context.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(PersistenceDBContext).Assembly.FullName)));

            services.AddDbContext<DomainDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(DomainDbContext).Assembly.FullName)));

            services.AddScoped<IDatabase, DomainDbContext>();

            services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

            services.AddScoped<DepositOperation, DepositOperation>();
            services.AddScoped<WithdrawOperation, WithdrawOperation>();
            services.AddScoped<TransferOperation, TransferOperation>();
            services.AddScoped<IMovementRepository, MovementRepository>();
            services.AddScoped<IExchangeRateCacheRepository, ExchangeRateCacheRepository>();
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

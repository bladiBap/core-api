using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Currencies.Entities;
using TestDevCore.Domain.ExchangeRates.Entities;
using TestDevCore.Domain.Movements.Entities;

namespace TestDevCore.Infrastructure.Persistence.DomainModel
{
    internal class DomainDbContext : DbContext
    {
        public DbSet<Currency> Currencies{ get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<MovementDetail> MovementDetails { get; set; }

        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

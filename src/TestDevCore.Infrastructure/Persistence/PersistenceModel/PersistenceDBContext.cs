using Microsoft.EntityFrameworkCore;
using TestDevCore.Infrastructure.Persistence.PersistenceModel.Models;

namespace TestDevCore.Infrastructure.Persistence.PersistenceModel
{
    internal class PersistenceDBContext : DbContext
    {
        public DbSet<AccountPersistence> Accounts { get; set; }
        public DbSet<CurrencyPersistence> Currencies { get; set; }
        public DbSet<ExchangeRatePersistence> ExchangeRates { get; set; }
        public DbSet<MovementPersistence> Movements { get; set; }
        public DbSet<MovementDetailPersistence> MovementDetails { get; set; }

        public PersistenceDBContext(DbContextOptions<PersistenceDBContext> options) : base(options)
        {
        }
    }
}

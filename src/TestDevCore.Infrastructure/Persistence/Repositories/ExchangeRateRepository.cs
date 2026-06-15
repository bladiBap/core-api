using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestDevCore.Domain.ExchangeRates.Entities;
using TestDevCore.Domain.ExchangeRates.Repositories;
using TestDevCore.Infrastructure.Persistence.DomainModel;

namespace TestDevCore.Infrastructure.Persistence.Repositories
{
    internal class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly DomainDbContext _dbContext;

        public ExchangeRateRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(ExchangeRate exchangeRate, CancellationToken ct = default)
        {
            await _dbContext.ExchangeRates.AddAsync(exchangeRate, ct);
        }

        public async Task<ExchangeRate?> GetByPairAsync(string symbolBase, string symbolTarget, CancellationToken ct = default)
        {
            var exchangeRate = await _dbContext.ExchangeRates
                    .FirstOrDefaultAsync(x => x.SymbolBase == symbolBase.ToUpper() && x.SymbolTarget == symbolTarget.ToUpper(), ct);
            return exchangeRate;
        }

        public void Save(ExchangeRate exchangeRate)
        {
            _dbContext.ExchangeRates.Update(exchangeRate);
        }
    }
}

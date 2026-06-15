
using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Currencies.Entities;
using TestDevCore.Domain.ExchangeRates.Entities;

namespace TestDevCore.Domain.ExchangeRates.Repositories
{
    public interface IExchangeRateRepository
    {
        Task AddAsync(ExchangeRate exchangeRate, CancellationToken ct = default);
        void Save(ExchangeRate exchangeRate);
        Task<ExchangeRate?> GetByPairAsync(string symbolBase, string symbolTarget, CancellationToken ct = default);
    }
}

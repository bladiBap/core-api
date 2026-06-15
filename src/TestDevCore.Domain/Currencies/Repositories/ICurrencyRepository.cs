using TestDevCore.Domain.Currencies.Entities;

namespace TestDevCore.Domain.Currencies.Repositories
{
    public interface ICurrencyRepository
    {
         Task<Currency?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using TestDevCore.Application.Currencies.DTOs;
using TestDevCore.Application.Currencies.Queries.GetAllCurrencies;
using TestDevCore.Core.Results;
using TestDevCore.Infrastructure.Persistence.PersistenceModel;

namespace TestDevCore.Infrastructure.Queries.Currencies
{
    internal class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrenciesQuery, Result<IEnumerable<CurrencyDTO>>>
    {
        private readonly PersistenceDBContext _dbContext;

        public GetAllCurrenciesHandler(PersistenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<CurrencyDTO>>> Handle(GetAllCurrenciesQuery request, CancellationToken ct)
        {
            var currencies = await _dbContext.Currencies
                .AsNoTracking()
                .OrderBy(currency => currency.Symbol)
                .Select(currency => new CurrencyDTO(
                    currency.Id,
                    currency.Symbol,
                    currency.Name))
                .ToListAsync(ct);

            return Result.Success<IEnumerable<CurrencyDTO>>(currencies);
        }
    }
}
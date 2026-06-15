using MediatR;
using TestDevCore.Application.Currencies.DTOs;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.Currencies.Queries.GetAllCurrencies
{
    public record GetAllCurrenciesQuery() : IRequest<Result<IEnumerable<CurrencyDTO>>>;
}
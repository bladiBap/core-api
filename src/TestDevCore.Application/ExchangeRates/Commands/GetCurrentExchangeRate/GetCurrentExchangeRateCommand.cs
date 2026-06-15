using MediatR;
using TestDevCore.Application.ExchangeRates.DTOs;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.ExchangeRates.Commands.GetCurrentExchangeRate
{
    public record GetCurrentExchangeRateCommand(
        string SymbolBase,
        string SymbolTarget
    ) : IRequest<Result<ExchangeRateDTO>>;
}
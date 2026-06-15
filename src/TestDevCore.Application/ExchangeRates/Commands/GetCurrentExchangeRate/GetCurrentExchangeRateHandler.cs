using MediatR;
using TestDevCore.Application.ExchangeRates.DTOs;
using TestDevCore.Application.Services.ExchangeRates;
using TestDevCore.Core.Interfaces;
using TestDevCore.Core.Results;
using TestDevCore.Domain.ExchangeRates.Errors;

namespace TestDevCore.Application.ExchangeRates.Commands.GetCurrentExchangeRate
{
    internal class GetCurrentExchangeRateHandler : IRequestHandler<GetCurrentExchangeRateCommand, Result<ExchangeRateDTO>>
    {
        private readonly ExchangeRateService _exchangeRateService;
        private readonly IUnitOfWork _unitOfWork;

        public GetCurrentExchangeRateHandler(
            ExchangeRateService exchangeRateService,
            IUnitOfWork unitOfWork
        )
        {
            _exchangeRateService = exchangeRateService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ExchangeRateDTO>> Handle(GetCurrentExchangeRateCommand request, CancellationToken ct)
        {
            var exchangeRate = await _exchangeRateService.GetByPairAsync(
                request.SymbolBase,
                request.SymbolTarget,
                ct);

            if (exchangeRate is null)
            {
                return Result.Failure<ExchangeRateDTO>(ExchangeRateError.NotFound);
            }

            await _unitOfWork.CommitAsync(ct);

            var dto = new ExchangeRateDTO(
                exchangeRate.Id,
                exchangeRate.SymbolBase,
                exchangeRate.SymbolTarget,
                exchangeRate.Rate,
                exchangeRate.CreatedAt,
                exchangeRate.UpdatedAt);

            return Result.Success(dto);
        }
    }
}
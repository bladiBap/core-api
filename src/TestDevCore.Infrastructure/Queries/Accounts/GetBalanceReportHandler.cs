using MediatR;
using Microsoft.EntityFrameworkCore;
using TestDevCore.Application.Accounts.DTOs;
using TestDevCore.Application.Accounts.Queries.GetBalanceReport;
using TestDevCore.Application.Services.ExchangeRates;
using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.Errors;
using TestDevCore.Domain.ExchangeRates.Errors;
using TestDevCore.Infrastructure.Persistence.PersistenceModel;

namespace TestDevCore.Infrastructure.Queries.Accounts
{
    internal class GetBalanceReportHandler : IRequestHandler<GetBalanceReportQuery, Result<BalanceReportDTO>>
    {
        private static readonly HashSet<string> AllowedCurrencies = new(StringComparer.OrdinalIgnoreCase)
        {
            "BOB",
            "USD"
        };

        private readonly PersistenceDBContext _dbContext;
        private readonly ExchangeRateService _exchangeRateService;

        public GetBalanceReportHandler(
            PersistenceDBContext dbContext,
            ExchangeRateService exchangeRateService)
        {
            _dbContext = dbContext;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<Result<BalanceReportDTO>> Handle(GetBalanceReportQuery request, CancellationToken ct)
        {
            var startDateUtc = NormalizeToUtc(request.StartDate);
            var endDateUtc = NormalizeToUtc(request.EndDate);

            if (startDateUtc > endDateUtc)
            {
                return Result.Failure<BalanceReportDTO>(
                    Error.Validation(
                        "AccountBalanceReport.DateRange.Invalid",
                        "Start date must be less than or equal to end date."));
            }

            var targetCurrency = request.Currency.ToUpperInvariant();
            if (!AllowedCurrencies.Contains(targetCurrency))
            {
                return Result.Failure<BalanceReportDTO>(
                    Error.Validation(
                        "AccountBalanceReport.Currency.Invalid",
                        "Currency must be BOB or USD."));
            }

            var account = await _dbContext.Accounts
                .AsNoTracking()
                .Include(account => account.Currency)
                .FirstOrDefaultAsync(account => account.Id == request.AccountId, ct);

            if (account is null)
            {
                return Result.Failure<BalanceReportDTO>(AccountError.NotFound);
            }

            var sourceCurrency = account.Currency.Symbol.ToUpperInvariant();
            decimal conversionRate = 1m;

            if (!string.Equals(sourceCurrency, targetCurrency, StringComparison.OrdinalIgnoreCase))
            {
                var exchangeRate = await _exchangeRateService.GetByPairAsync(sourceCurrency, targetCurrency, ct);

                if (exchangeRate is null)
                {
                    return Result.Failure<BalanceReportDTO>(ExchangeRateError.NotFound);
                }

                conversionRate = exchangeRate.Rate;
            }

            var movementRows = await (
                from detail in _dbContext.MovementDetails.AsNoTracking()
                join movement in _dbContext.Movements.AsNoTracking() on detail.MovementId equals movement.Id
                where detail.AccountId == request.AccountId
                    && movement.CreatedAt >= startDateUtc
                    && movement.CreatedAt <= endDateUtc
                orderby movement.CreatedAt
                select new
                {
                    MovementDetailId = detail.Id,
                    MovementId = movement.Id,
                    movement.Description,
                    movement.Type,
                    detail.Amount,
                    detail.PreviousBalance,
                    detail.NewBalance,
                    movement.CreatedAt
                })
                .ToListAsync(ct);

            decimal ConvertAmount(decimal value) =>
                Math.Round(value * conversionRate, 2, MidpointRounding.AwayFromZero);

            var movements = movementRows
                .Select(row => new BalanceReportMovementDTO(
                    row.MovementDetailId,
                    row.MovementId,
                    row.Description,
                    row.Type,
                    ConvertAmount(row.Amount),
                    ConvertAmount(row.PreviousBalance),
                    ConvertAmount(row.NewBalance),
                    row.CreatedAt))
                .ToList();

            decimal openingBalance = movements.Count > 0
                ? movements.First().PreviousBalance
                : ConvertAmount(account.Balance);

            decimal closingBalance = movements.Count > 0
                ? movements.Last().NewBalance
                : ConvertAmount(account.Balance);

            var report = new BalanceReportDTO(
                account.Id,
                sourceCurrency,
                targetCurrency,
                startDateUtc,
                endDateUtc,
                openingBalance,
                closingBalance - openingBalance,
                closingBalance,
                movements.Count,
                movements);

            return Result.Success(report);
        }

        private static DateTime NormalizeToUtc(DateTime dateTime)
        {
            return dateTime.Kind switch
            {
                DateTimeKind.Utc => dateTime,
                DateTimeKind.Local => dateTime.ToUniversalTime(),
                _ => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
            };
        }
    }
}
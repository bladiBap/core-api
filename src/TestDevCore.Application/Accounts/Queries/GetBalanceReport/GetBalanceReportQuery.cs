using MediatR;
using TestDevCore.Application.Accounts.DTOs;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.Accounts.Queries.GetBalanceReport
{
    public record GetBalanceReportQuery(
        Guid AccountId,
        DateTime StartDate,
        DateTime EndDate,
        string Currency
    ) : IRequest<Result<BalanceReportDTO>>;
}
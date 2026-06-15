using MediatR;
using TestDevCore.Application.Accounts.DTOs;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.Accounts.Commands.Create
{
    public record CreateAccountCommand
    (
        string holder,
        decimal balance,
        Guid currencyId
    ) : IRequest<Result<AccountDTO>>;
}

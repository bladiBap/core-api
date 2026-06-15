using MediatR;
using TestDevCore.Application.Accounts.DTOs;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.Accounts.Queries.GetById
{
    public record GetAccountByIdQuery
    (
        Guid Id
    ) : IRequest<Result<AccountDTO>>;
}

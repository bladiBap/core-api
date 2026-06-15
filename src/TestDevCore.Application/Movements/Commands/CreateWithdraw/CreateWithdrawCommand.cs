using MediatR;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.Movements.Commands.CreateWithdraw
{
    public record CreateWithdrawCommand(
        Guid AccountId,
        decimal Amount,
        string Description
    ) : IRequest<Result<Guid>>;
}
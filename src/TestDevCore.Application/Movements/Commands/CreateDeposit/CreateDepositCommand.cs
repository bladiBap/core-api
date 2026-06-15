using MediatR;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.Movements.Commands.CreateDeposit
{
    public record CreateDepositCommand(
        Guid AccountId,
        decimal Amount,
        string Description
    ) : IRequest<Result<Guid>>;
}

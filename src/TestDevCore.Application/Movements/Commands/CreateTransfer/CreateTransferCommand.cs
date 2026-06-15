using MediatR;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.Movements.Commands.CreateTransfer
{
    public record CreateTransferCommand(
        Guid SourceAccountId,
        Guid DestinationAccountId,
        decimal Amount,
        string Description
    ) : IRequest<Result<Guid>>;
}
using TestDevCore.Core.Results;

namespace TestDevCore.Domain.Services.Movements.Operations.Errors
{
    public class OperationError
    {
        public static Error DestinationAccountIsRequired =>
            Error.Validation(
                "Operation.DestinationAccount.Required",
                "Destination Account is required"
            );
    }
}

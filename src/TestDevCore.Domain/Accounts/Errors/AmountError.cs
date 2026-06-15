using TestDevCore.Core.Results;

namespace TestDevCore.Domain.Accounts.Errors
{
    public static class AmountError
    {
        public static Error AmountCannotBeLessThanZero =>
            Error.Validation(
                code: "Amount.LessThanZero",
                message: "The Amount cannot be less than zero."
            );
    }
}

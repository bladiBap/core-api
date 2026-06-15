using TestDevCore.Core.Results;

namespace TestDevCore.Domain.Accounts.Errors
{
    public static class AccountError
    {
        public static Error NotFound =>
            Error.NotFound(
                "Account.NotFound",
                "The specified Account was not found."
            );
        public static Error HolderIsRequired =>
            Error.Validation(
                "Account.Holder.Required",
                "Account Holder is required"
            );
        public static Error HolderInvalidLength(int min, int max) =>
            Error.Validation(
                "Account.Holder.Length",
                $"Holder must be between {min} and {max} characters."
            );
        public static Error AccountLocked =>
            Error.Failure(
                code: "Account.Locked",
                message: "The Account is locked."
            );
        public static Error InsufficientFunds =>
            Error.Failure(
                code: "Account.InsufficientFunds",
                message: "The Account has insufficient funds for this operation."
            );
    }
}

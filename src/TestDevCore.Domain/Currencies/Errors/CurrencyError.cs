using TestDevCore.Core.Results;

namespace TestDevCore.Domain.Currencies.Errors
{
    public static class CurrencyError
    {
        public static Error NotFound =>
            Error.NotFound(
                "Currency.NotFound",
                "The specified Currency was not found."
            );
        public static Error SymbolIsEmpty =>
            Error.Validation(
                "Currency.Symbol.IsEmpty",
                "Symbol cannot be empty."
            );
    }
}

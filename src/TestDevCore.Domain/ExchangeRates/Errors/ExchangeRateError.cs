using TestDevCore.Core.Results;

namespace TestDevCore.Domain.ExchangeRates.Errors
{
    public static class ExchangeRateError
    {
        public static Error NotFound =>
            Error.NotFound(
                "ExchangeRate.NotFound",
                "Exchange rate not found."
            );
        public static Error SymbolIsRequired =>
            Error.Validation(
                "ExchangeRate.Symbol.Required",
                "Symbol is required"
            );
        public static Error SymbolInvalidLength(int min, int max) =>
            Error.Validation(
                "ExchangeRate.Symbol.Length",
                $"Symbol must be between {min} and {max} characters."
            );
        
        public static Error RateMustBeNonLessZero =>
            Error.Validation(
                "ExchangeRate.Rate.NonLessZero",
                "Rate must be NonLessZero."
            );
    }
}

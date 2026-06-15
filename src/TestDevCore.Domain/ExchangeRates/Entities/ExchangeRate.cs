using TestDevCore.Core.Abstractions;
using TestDevCore.Core.Results;
using TestDevCore.Domain.ExchangeRates.Errors;

namespace TestDevCore.Domain.ExchangeRates.Entities
{
    public class ExchangeRate : AggregateRoot
    {

        public string SymbolBase { get; private set; }
        public string SymbolTarget { get; private set; }
        public decimal Rate { get; private set; }

        private ExchangeRate()
        {
        }

        private ExchangeRate(string symbolBase, string symbolTarget, decimal rate) 
            :base(Guid.NewGuid()){
            SymbolBase = symbolBase;
            SymbolTarget = symbolTarget;
            Rate = rate;
        }

        public static ExchangeRate Create(string symbolBase, string symbolTarget, decimal rate)
        {
            ValidateSymbol(symbolBase);
            ValidateSymbol(symbolTarget);
            return new ExchangeRate(symbolBase, symbolTarget, rate);
        }

        public void ChangeRate(decimal newRate)
        {

            if (newRate <= 0) 
            {
                throw new DomainException(ExchangeRateError.RateMustBeNonLessZero);
            }
            MarkAsUpdated();
            Rate = newRate;
        }

        private static void ValidateSymbol(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new DomainException(ExchangeRateError.SymbolIsRequired);
            }
            if (symbol.Length < 1 || symbol.Length > 10)
            {
                throw new DomainException(ExchangeRateError.SymbolInvalidLength(1, 5));
            }
        }
    }
}

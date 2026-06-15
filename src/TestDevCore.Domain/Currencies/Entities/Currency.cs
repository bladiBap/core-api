using TestDevCore.Core.Abstractions;
using TestDevCore.Core.Results;
using TestDevCore.Domain.Currencies.Errors;

namespace TestDevCore.Domain.Currencies.Entities
{
    public class Currency : AggregateRoot
    {

        public string Symbol { get; private set; }
        public string Name { get; private set; }

        private Currency()
        {
        }

        private Currency(string symbol, string name) :
            base(Guid.NewGuid())
        {
            Symbol = symbol;
            Name = name;
        }

        public static Currency Create(string symbol, string name)
        {
            ValidateSymbol(symbol);
            return new Currency(symbol, name);
        }

        public void SetSymbol(string symbol)
        {
            ValidateSymbol(symbol);
            Symbol = symbol;
            MarkAsUpdated();
        }

        private static void ValidateSymbol(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new DomainException(CurrencyError.SymbolIsEmpty);
            }
        }
    }
}

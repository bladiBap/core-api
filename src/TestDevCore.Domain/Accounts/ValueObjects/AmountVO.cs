using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.Errors;

namespace TestDevCore.Domain.Accounts.ValueObjects
{
    public record AmountVO
    {
        public decimal Value { get; }

        private AmountVO(decimal value)
        {
            Value = value;
        }

        public static AmountVO Create(decimal value) {
            ValidatePrice(value);
            return new AmountVO(value);
        }

        private static void ValidatePrice(decimal value)
        {
            if (value < 0)
            {
                throw new DomainException(AmountError.AmountCannotBeLessThanZero);
            }
        }
    }
}

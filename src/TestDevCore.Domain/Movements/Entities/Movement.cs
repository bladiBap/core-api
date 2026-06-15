using TestDevCore.Core.Abstractions;
using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.ValueObjects;
using TestDevCore.Domain.Movements.Enums;
using TestDevCore.Domain.Movements.Errors;

namespace TestDevCore.Domain.Movements.Entities
{
    public class Movement : AggregateRoot
    {
        public string Description { get; private set; }
        public AmountVO ExchangeRate { get; private set; }
        public MovementType Type { get; private set; }
        public List<MovementDetail> MovementDetails { get; private set; }

        private Movement(){}

        private Movement(string description, MovementType type, List<MovementDetail> movementDetails, AmountVO exchangeRate)
            :base(Guid.NewGuid()){
            
            Description = description;
            ExchangeRate = exchangeRate;
            MovementDetails = movementDetails;
            Type = type;
        }

        public static Movement Create(string description, MovementType type, AmountVO exchangeRate)
        {
            ValidateDescription(description);
            return new Movement(description, type, [], exchangeRate);
        }

        public void AddDetail( Guid accountId, AmountVO amount, AmountVO newBalance, AmountVO previousBalance)
        {
            MovementDetail newMovementDetail = MovementDetail.Create(Id ,accountId, amount, newBalance, previousBalance);
            MovementDetails.Add(newMovementDetail);
        }

        private static void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new DomainException(MovementError.DescriptionIsRequired);
            }
            if (description.Length < 3 || description.Length > 30)
            {
                throw new DomainException(MovementError.DescriptionInvalidLength(3, 30));
            }
        }
    }
}

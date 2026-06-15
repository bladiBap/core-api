using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Accounts.ValueObjects;
using TestDevCore.Domain.Movements.Entities;
using TestDevCore.Domain.Movements.Enums;
using TestDevCore.Domain.Services.Movements.Operations.Enums;
using TestDevCore.Domain.Services.Movements.Operations.Errors;
using TestDevCore.Domain.Services.Movements.Operations.Interfaces;

namespace TestDevCore.Domain.Services.Movements.Operations
{
    public class TransferOperation : IOperation
    {
        public OperationType Type => OperationType.Transfer;

        public Movement Process(string description, decimal amount, decimal exchangeRate, Account sourceAccount, Account? destinationAccount)
        {
            AmountVO sourceAmountVO = AmountVO.Create(amount);
            AmountVO exchangeRateVO = AmountVO.Create(exchangeRate);

            if (destinationAccount == null) {
                throw new DomainException(OperationError.DestinationAccountIsRequired);
            }

            Movement movement = Movement.Create(description, MovementType.TRANSFER, exchangeRateVO);

            AmountVO currentBalanceSourceAccount = sourceAccount.Balance;
            sourceAccount.Withdraw(sourceAmountVO);
            AmountVO newBalanceSourceAccount = sourceAccount.Balance;
            movement.AddDetail(sourceAccount.Id, AmountVO.Create(sourceAmountVO.Value), newBalanceSourceAccount, currentBalanceSourceAccount);

            AmountVO destinationAmountVO = sourceAmountVO;
            if (sourceAccount.CurrencyId != destinationAccount.CurrencyId)
            {
                destinationAmountVO = AmountVO.Create(sourceAmountVO.Value * exchangeRateVO.Value);
            }

            AmountVO currentBalanceDestinationAccount = destinationAccount.Balance;
            destinationAccount.Deposit(destinationAmountVO);
            AmountVO newBalanceDestinationAccount = destinationAccount.Balance;
            movement.AddDetail(destinationAccount.Id, AmountVO.Create(destinationAmountVO.Value), newBalanceDestinationAccount, currentBalanceDestinationAccount);

            return movement;
        }
    }
}

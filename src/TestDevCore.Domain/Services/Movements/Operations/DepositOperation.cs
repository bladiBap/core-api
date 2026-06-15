using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Accounts.ValueObjects;
using TestDevCore.Domain.Movements.Entities;
using TestDevCore.Domain.Movements.Enums;
using TestDevCore.Domain.Services.Movements.Operations.Enums;
using TestDevCore.Domain.Services.Movements.Operations.Interfaces;

namespace TestDevCore.Domain.Services.Movements.Operations
{
    public class DepositOperation : IOperation
    {
        public OperationType Type => OperationType.Deposit;

        public Movement Process(string description, decimal amount, decimal exchangeRate, Account sourceAccount, Account? destinationAccount)
        {
            AmountVO amountVO = AmountVO.Create(amount);
            AmountVO exchangeRateVO = AmountVO.Create(exchangeRate);

            AmountVO currentBalance = sourceAccount.Balance;
            sourceAccount.Deposit(amountVO);
            AmountVO newBalance = sourceAccount.Balance;

            Movement movement = Movement.Create(description, MovementType.DEPOSIT, exchangeRateVO);
            movement.AddDetail(sourceAccount.Id, amountVO, newBalance, currentBalance);

            return movement;
        }
    }
}

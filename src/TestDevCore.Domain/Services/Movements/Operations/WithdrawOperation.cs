using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Accounts.ValueObjects;
using TestDevCore.Domain.Movements.Entities;
using TestDevCore.Domain.Movements.Enums;
using TestDevCore.Domain.Services.Movements.Operations.Enums;
using TestDevCore.Domain.Services.Movements.Operations.Interfaces;

namespace TestDevCore.Domain.Services.Movements.Operations
{
    public class WithdrawOperation : IOperation
    {
        public OperationType Type => OperationType.Withdraw;

        public Movement Process(string description, decimal amount, decimal exchangeRate, Account sourceAccount, Account? destinationAccount)
        {
            AmountVO amountVO = AmountVO.Create(amount);
            AmountVO exchangeRateVO = AmountVO.Create(exchangeRate);
            
            AmountVO currentBalance = sourceAccount.Balance;
            sourceAccount.Withdraw(amountVO);
            AmountVO newBalance = sourceAccount.Balance;

            Movement movement = Movement.Create(description, MovementType.WITHDRAWAL, exchangeRateVO);
            movement.AddDetail(sourceAccount.Id, amountVO, newBalance, currentBalance);
            return movement;
        }
    }
}

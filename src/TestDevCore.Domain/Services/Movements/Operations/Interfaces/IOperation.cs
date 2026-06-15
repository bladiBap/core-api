using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Movements.Entities;
using TestDevCore.Domain.Services.Movements.Operations.Enums;

namespace TestDevCore.Domain.Services.Movements.Operations.Interfaces
{
    public interface IOperation
    {
        OperationType Type { get; }
        Movement Process(string description, decimal amount, decimal exchangeRate, Account sourceAccount, Account? destinationAccount);
    }
}

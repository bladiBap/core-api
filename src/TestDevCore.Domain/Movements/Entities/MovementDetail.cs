using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Core.Abstractions;
using TestDevCore.Domain.Accounts.ValueObjects;

namespace TestDevCore.Domain.Movements.Entities
{
    public class MovementDetail : Entity
    {
        public Guid MovementId { get; private set; }
        public Guid AccountId { get; private set; }
        public AmountVO Amount { get; private set; }
        public AmountVO NewBalance { get; private set; }
        public AmountVO PreviousBalance { get; private set; }

        private MovementDetail() { }

        private MovementDetail(Guid movementId, Guid accountId, AmountVO amount, AmountVO newBalance, AmountVO previousBalance)
            : base(Guid.NewGuid())
        {
            AccountId = accountId;
            Amount = amount;
            NewBalance = newBalance;
            MovementId = movementId;
            PreviousBalance = previousBalance;
        }

        public static MovementDetail Create(Guid movementId, Guid accountId, AmountVO amount, AmountVO newBalance, AmountVO previousBalance)
        {
            return new MovementDetail(movementId, accountId, amount, newBalance, previousBalance);
        }
    }
}

using TestDevCore.Core.Abstractions;
using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.Errors;
using TestDevCore.Domain.Accounts.ValueObjects;

namespace TestDevCore.Domain.Accounts.Entities
{
    public class Account : AggregateRoot
    {
        public AmountVO Balance { get; private set; }
        public Guid CurrencyId { get; private set; }
        public string Holder { get; private set; }
        public bool IsActive { get; private set; }

        private Account() { }

        private Account(AmountVO balance, Guid currencyId, string holder, bool isActive)
            :base(Guid.NewGuid()) {
            Balance = balance;
            CurrencyId = currencyId;
            Holder = holder;
            IsActive = isActive;
        }

        public static Account Create(AmountVO balance, Guid currencyId, string holder)
        {
            ValidateHolder(holder);
            return new Account(balance, currencyId, holder, true);
        }

        public void ChangeHolder(string newHolder)
        {
            EnsureAccountIsActive();
            ValidateHolder(newHolder);
            Holder = newHolder;
        }

        public void Lock()
        {
            IsActive = false;
        }

        public void Unlock()
        {
            IsActive = true;
        }

        public bool HaveSufficientBalance(AmountVO amount)
        {
            return Balance.Value >= amount.Value;
        }

        public void Withdraw(AmountVO amount)
        {
            EnsureAccountIsActive();
            if (!HaveSufficientBalance(amount))
            {
                throw new DomainException(AccountError.InsufficientFunds);
            }
            Balance = AmountVO.Create(Balance.Value - amount.Value);
        }

        public void Deposit(AmountVO amount)
        {
            EnsureAccountIsActive();
            Balance = AmountVO.Create(Balance.Value + amount.Value);
        }

        public void EnsureAccountIsActive()
        {
            if (!IsActive)
            {
                throw new DomainException(AccountError.AccountLocked);
            }
        }

        private static void ValidateHolder(string holder)
        {
            if (string.IsNullOrWhiteSpace(holder))
            {
                throw new DomainException(AccountError.HolderIsRequired);
            }
            if (holder.Length < 3 || holder.Length > 30)
            {
                throw new DomainException(AccountError.HolderInvalidLength(3, 30));
            }
        }
    }
}

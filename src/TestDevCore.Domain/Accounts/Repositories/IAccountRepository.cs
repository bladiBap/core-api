using TestDevCore.Domain.Accounts.Entities;
namespace TestDevCore.Domain.Accounts.Repositories
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account, CancellationToken ct = default);
        void SaveAsync(Account account, CancellationToken ct = default);
        Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}

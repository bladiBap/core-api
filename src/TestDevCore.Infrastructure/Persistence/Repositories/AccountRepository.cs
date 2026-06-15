using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Accounts.Repositories;
using TestDevCore.Infrastructure.Persistence.DomainModel;

namespace TestDevCore.Infrastructure.Persistence.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly DomainDbContext _dbContext;
        public AccountRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Account account, CancellationToken ct = default)
        {
            await _dbContext.Accounts.AddAsync(account, ct);
        }

        public async Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(p => p.Id == id, ct);
            return account;
        }

        public void SaveAsync(Account account, CancellationToken ct = default)
        {
            _dbContext.Accounts.Update(account);
        }
    }
}

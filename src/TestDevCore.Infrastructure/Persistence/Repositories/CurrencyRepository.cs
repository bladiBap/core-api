using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Domain.Currencies.Entities;
using TestDevCore.Domain.Currencies.Repositories;
using TestDevCore.Infrastructure.Persistence.DomainModel;

namespace TestDevCore.Infrastructure.Persistence.Repositories
{
    internal class CurrencyRepository : ICurrencyRepository
    {
        private readonly DomainDbContext _dbContext;
        public CurrencyRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Currency?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var currency = await _dbContext.Currencies
                .FirstOrDefaultAsync(c => c.Id == id, ct);

            return currency;
        }
    }
}

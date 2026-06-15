using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Application.Accounts.DTOs;
using TestDevCore.Application.Accounts.Queries.GetById;
using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.Errors;
using TestDevCore.Infrastructure.Persistence.PersistenceModel;

namespace TestDevCore.Infrastructure.Queries.Accounts
{
    internal class GetAccountByIdHandler :
        IRequestHandler<GetAccountByIdQuery, Result<AccountDTO>>
    {
        private readonly PersistenceDBContext _dbContext;

        public GetAccountByIdHandler(PersistenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<AccountDTO>> Handle(GetAccountByIdQuery request, CancellationToken ct)
        {
            var account = await _dbContext.Accounts
                .AsNoTracking()
                .Where(p => p.Id == request.Id)
                .Select(p => new AccountDTO(
                    p.Id,
                    p.Balance,
                    p.Currency.Symbol,
                    p.Holder,
                    p.IsActive
                ))
                .FirstOrDefaultAsync(ct);

            
            if (account == null)
            {
                return Result.Failure<AccountDTO>(
                    AccountError.NotFound
                );
            }

            return Result.Success(account);
        }
    }
}

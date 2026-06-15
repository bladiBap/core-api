using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Application.Accounts.DTOs;
using TestDevCore.Core.Interfaces;
using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Accounts.Repositories;
using TestDevCore.Domain.Accounts.ValueObjects;
using TestDevCore.Domain.Currencies.Errors;
using TestDevCore.Domain.Currencies.Repositories;

namespace TestDevCore.Application.Accounts.Commands.Create
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Result<AccountDTO>>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateAccountHandler(
            ICurrencyRepository currencyRepository,
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork
        )
        {
            _currencyRepository = currencyRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AccountDTO>> Handle(CreateAccountCommand request, CancellationToken ct)
        {
            var currency = await _currencyRepository.GetByIdAsync(request.currencyId, ct);

            if (currency == null)
            {
                return Result.Failure<AccountDTO>(CurrencyError.NotFound);
            }

            var balance = AmountVO.Create(request.balance);
            var holder = request.holder;
            var currencyId = request.currencyId;

            var account = Account.Create(
                balance,
                currencyId,
                holder
            );

            await _accountRepository.AddAsync(account, ct);
            await _unitOfWork.CommitAsync(ct);

            var dto = new AccountDTO(
                account.Id,
                account.Balance.Value,
                currency.Symbol,
                account.Holder,
                account.IsActive
            );

            return Result.Success(dto);
        }
    }
}

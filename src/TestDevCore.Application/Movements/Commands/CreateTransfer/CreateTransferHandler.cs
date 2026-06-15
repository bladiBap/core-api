using MediatR;
using TestDevCore.Core.Interfaces;
using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.Errors;
using TestDevCore.Domain.Accounts.Repositories;
using TestDevCore.Domain.Movements.Repositories;
using TestDevCore.Domain.Services.Movements.Operations;
using TestDevCore.Application.Services.ExchangeRates;
using TestDevCore.Domain.Currencies.Repositories;
using TestDevCore.Domain.Currencies.Errors;
using TestDevCore.Domain.ExchangeRates.Errors;
using TestDevCore.Domain.Movements.Errors;

namespace TestDevCore.Application.Movements.Commands.CreateTransfer
{
    internal class CreateTransferHandler : IRequestHandler<CreateTransferCommand, Result<Guid>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly TransferOperation _transferOperation;
        private readonly ExchangeRateService _exchangeRateService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransferHandler(
            IAccountRepository accountRepository,
            IMovementRepository movementRepository,
            ICurrencyRepository currencyRepository,
            TransferOperation transferOperation,
            ExchangeRateService exchangeRateService,
            IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _movementRepository = movementRepository;
            _currencyRepository = currencyRepository;
            _transferOperation = transferOperation;
            _exchangeRateService = exchangeRateService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateTransferCommand request, CancellationToken ct)
        {

            if (request.SourceAccountId == request.DestinationAccountId)
            {
                return Result.Failure<Guid>(MovementError.SameAccountTransfer);
            }
            var sourceAccount = await _accountRepository.GetByIdAsync(request.SourceAccountId, ct);

            if (sourceAccount is null)
            {
                return Result.Failure<Guid>(AccountError.NotFound);
            }

            var destinationAccount = await _accountRepository.GetByIdAsync(request.DestinationAccountId, ct);

            if (destinationAccount is null)
            {
                return Result.Failure<Guid>(AccountError.NotFound);
            }

            var currecyBase = await _currencyRepository.GetByIdAsync(sourceAccount.CurrencyId, ct);
            var currencyTarget = await _currencyRepository.GetByIdAsync(destinationAccount.CurrencyId, ct);

            if (currecyBase is null || currencyTarget is null)
            {
                return Result.Failure<Guid>(CurrencyError.NotFound);
            }

            var exchangeRate = await _exchangeRateService.GetByPairAsync(currecyBase.Symbol, currencyTarget.Symbol, ct);

            if (exchangeRate is null)
            {
                return Result.Failure<Guid>(ExchangeRateError.NotFound);
            }

            var movement = _transferOperation.Process(
                request.Description,
                request.Amount,
                exchangeRate.Rate,
                sourceAccount,
                destinationAccount);

            _accountRepository.SaveAsync(sourceAccount, ct);
            _accountRepository.SaveAsync(destinationAccount, ct);
            await _movementRepository.AddAsync(movement, ct);
            await _unitOfWork.CommitAsync(ct);

            return Result.Success(movement.Id);
        }
    }
}
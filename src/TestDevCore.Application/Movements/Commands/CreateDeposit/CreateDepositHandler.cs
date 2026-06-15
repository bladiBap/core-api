using MediatR;
using TestDevCore.Core.Interfaces;
using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.Errors;
using TestDevCore.Domain.Accounts.Repositories;
using TestDevCore.Domain.Movements.Repositories;
using TestDevCore.Domain.Services.Movements.Operations;

namespace TestDevCore.Application.Movements.Commands.CreateDeposit
{
    internal class CreateDepositHandler : IRequestHandler<CreateDepositCommand, Result<Guid>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly DepositOperation _depositOperation;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepositHandler(
            IAccountRepository accountRepository,
            IMovementRepository movementRepository,
            DepositOperation depositOperation,
            IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _movementRepository = movementRepository;
            _unitOfWork = unitOfWork;
            _depositOperation = depositOperation;
        }

        public async Task<Result<Guid>> Handle(CreateDepositCommand request, CancellationToken ct)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId, ct);

            if (account is null)
            {
                return Result.Failure<Guid>(AccountError.NotFound);
            }

            var movement = _depositOperation.Process(
                request.Description,
                request.Amount,
                1m,
                account,
                null);

            _accountRepository.SaveAsync(account, ct);
            await _movementRepository.AddAsync(movement, ct);
            await _unitOfWork.CommitAsync(ct);

            return Result.Success(movement.Id);
        }
    }
}
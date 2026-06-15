using MediatR;
using TestDevCore.Core.Interfaces;
using TestDevCore.Core.Results;
using TestDevCore.Domain.Accounts.Errors;
using TestDevCore.Domain.Accounts.Repositories;
using TestDevCore.Domain.Movements.Repositories;
using TestDevCore.Domain.Services.Movements.Operations;

namespace TestDevCore.Application.Movements.Commands.CreateWithdraw
{
    internal class CreateWithdrawHandler : IRequestHandler<CreateWithdrawCommand, Result<Guid>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly WithdrawOperation _withdrawOperation;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWithdrawHandler(
            IAccountRepository accountRepository,
            IMovementRepository movementRepository,
            WithdrawOperation withdrawOperation,
            IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _movementRepository = movementRepository;
            _withdrawOperation = withdrawOperation;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateWithdrawCommand request, CancellationToken ct)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId, ct);

            if (account is null)
            {
                return Result.Failure<Guid>(AccountError.NotFound);
            }

            var movement = _withdrawOperation.Process(
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
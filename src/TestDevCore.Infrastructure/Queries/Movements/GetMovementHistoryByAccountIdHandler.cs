using MediatR;
using Microsoft.EntityFrameworkCore;
using TestDevCore.Application.Movements.DTOs;
using TestDevCore.Application.Movements.Query.GetMovementHistoryByAccount;
using TestDevCore.Core.Results;
using TestDevCore.Infrastructure.Persistence.PersistenceModel;

namespace TestDevCore.Infrastructure.Queries.Movements
{
    internal class GetMovementHistoryByAccountIdHandler :
        IRequestHandler<GetMovementHistoryByAccountQuery, Result<IEnumerable<MovementDTO>>>
    {
        private readonly PersistenceDBContext _dbContext;

        public GetMovementHistoryByAccountIdHandler(PersistenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<MovementDTO>>> Handle(
            GetMovementHistoryByAccountQuery request,
            CancellationToken ct)
        {
            var movements = await _dbContext.MovementDetails
                .AsNoTracking()
                .Where(detail => detail.AccountId == request.Id)
                .Join(
                    _dbContext.Movements.AsNoTracking(),
                    detail => detail.MovementId,
                    movement => movement.Id,
                    (detail, movement) => new
                    {
                        Detail = detail,
                        Movement = movement
                    })
                .OrderByDescending(combined => combined.Movement.CreatedAt)
                .Select(combined => new MovementDTO(
                    combined.Detail.Id,
                    combined.Movement.Id,
                    combined.Detail.AccountId,
                    combined.Movement.Description,
                    combined.Movement.Type,
                    combined.Movement.ExchangeRate,
                    combined.Detail.Amount,
                    combined.Detail.PreviousBalance,
                    combined.Detail.NewBalance,
                    combined.Movement.CreatedAt
                ))
                .ToListAsync(ct);

            return Result.Success<IEnumerable<MovementDTO>>(movements);
        }
    }
}

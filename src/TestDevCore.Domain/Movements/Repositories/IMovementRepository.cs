using TestDevCore.Domain.Movements.Entities;

namespace TestDevCore.Domain.Movements.Repositories
{
    public interface IMovementRepository
    {
        Task AddAsync(Movement movement, CancellationToken ct = default);
    }
}

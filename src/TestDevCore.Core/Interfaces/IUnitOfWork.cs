namespace TestDevCore.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken ct = default);
    }
}

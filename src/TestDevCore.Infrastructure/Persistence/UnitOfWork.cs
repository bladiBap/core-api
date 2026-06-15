using MediatR;
using TestDevCore.Core.Interfaces;
using TestDevCore.Infrastructure.Persistence.DomainModel;

namespace TestDevCore.Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DomainDbContext  _dbContext;
        private readonly IMediator _mediator;

        public UnitOfWork(DomainDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            try
            {

                await _dbContext.SaveChangesAsync(ct);
            }
            catch (Exception e) {

                var a = 1;
            }
        }
    }
}

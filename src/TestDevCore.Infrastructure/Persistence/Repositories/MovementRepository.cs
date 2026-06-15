using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Domain.Movements.Entities;
using TestDevCore.Domain.Movements.Repositories;
using TestDevCore.Infrastructure.Persistence.DomainModel;

namespace TestDevCore.Infrastructure.Persistence.Repositories
{
    internal class MovementRepository : IMovementRepository
    {
        private readonly DomainDbContext _dbContext;
        public MovementRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Movement movement, CancellationToken ct = default)
        {
            await _dbContext.Movements.AddAsync(movement, ct);
        }
    }
}

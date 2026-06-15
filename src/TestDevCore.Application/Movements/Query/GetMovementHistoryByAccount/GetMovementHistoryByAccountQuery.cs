using MediatR;
using System;
using System.Collections.Generic;
using TestDevCore.Application.Movements.DTOs;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.Movements.Query.GetMovementHistoryByAccount
{
    public record GetMovementHistoryByAccountQuery
    (
        Guid Id
    ) : IRequest<Result<IEnumerable<MovementDTO>>>;
}
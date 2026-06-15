using System;

namespace TestDevCore.Application.Movements.DTOs
{
    public record MovementDTO(
        Guid MovementDetailId,
        Guid MovementId,
        Guid AccountId,
        string Description,
        int Type,
        decimal ExchangeRate,
        decimal Amount,
        decimal PreviousBalance,
        decimal NewBalance,
        DateTime CreatedAt
    );
}
using System;
using System.Collections.Generic;

namespace TestDevCore.Application.Accounts.DTOs
{
    public record BalanceReportMovementDTO(
        Guid MovementDetailId,
        Guid MovementId,
        string Description,
        int Type,
        decimal Amount,
        decimal PreviousBalance,
        decimal NewBalance,
        DateTime CreatedAt
    );

    public record BalanceReportDTO(
        Guid AccountId,
        string SourceCurrency,
        string TargetCurrency,
        DateTime StartDate,
        DateTime EndDate,
        decimal OpeningBalance,
        decimal NetMovement,
        decimal ClosingBalance,
        int MovementsCount,
        IEnumerable<BalanceReportMovementDTO> Movements
    );
}
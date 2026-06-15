using System;

namespace TestDevCore.Application.ExchangeRates.DTOs
{
    public record ExchangeRateDTO(
        Guid Id,
        string SymbolBase,
        string SymbolTarget,
        decimal Rate,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );
}
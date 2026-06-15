using System;

namespace TestDevCore.Application.Currencies.DTOs
{
    public record CurrencyDTO(
        Guid Id,
        string Symbol,
        string Name
    );
}
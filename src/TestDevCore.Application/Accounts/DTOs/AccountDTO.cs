using System;
using System.Collections.Generic;
using System.Text;

namespace TestDevCore.Application.Accounts.DTOs
{
    public record AccountDTO(
        Guid Id,
        decimal Balance,
        string Currency,
        string Holder,
        bool IsActive
    );
}

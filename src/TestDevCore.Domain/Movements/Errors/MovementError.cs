using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Core.Results;

namespace TestDevCore.Domain.Movements.Errors
{
    public static class MovementError
    {
        public static Error DescriptionIsRequired =>
            Error.Validation(
                "Movement.Description.Required",
                "Movement Description is required"
            );
        public static Error DescriptionInvalidLength(int min, int max) =>
            Error.Validation(
                "Movement.Description.Length",
                $"Movement Description must be between {min} and {max} characters."
            );

        public static Error SameAccountTransfer =>
            Error.Failure(
                "Movement.Transfer.SameAccount",
                "The source account and the destination account cannot be the same."
            );
    }
}

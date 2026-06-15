using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestDevCore.Api.Contracts
{
    public record CreateMovementContract
    {
        [JsonPropertyName("amount")]
        [Required(ErrorMessage = "The deposit amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The deposit amount must be greater than zero.")]
        public required decimal Amount { get; init; }

        [JsonPropertyName("description")]
        [Required(ErrorMessage = "The deposit description is required.")]
        [StringLength(50, ErrorMessage = "The description cannot exceed 50 characters.")]
        public required string Description { get; init; }
    }
}

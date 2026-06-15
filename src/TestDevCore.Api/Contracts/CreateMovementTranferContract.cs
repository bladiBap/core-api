using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestDevCore.Api.Contracts
{
    public record CreateMovementTranferContract
    {
        [JsonPropertyName("sourceAccountId")]
        [Required(ErrorMessage = "The source account identifier is required.")]
        public required Guid SourceAccountId { get; init; }

        [JsonPropertyName("destinationAccountId")]
        [Required(ErrorMessage = "The destination account identifier is required.")]
        public required Guid DestinationAccountId { get; init; }

        [JsonPropertyName("amount")]
        [Required(ErrorMessage = "The transfer amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The transfer amount must be greater than zero.")]
        public required decimal Amount { get; init; }

        [JsonPropertyName("description")]
        [Required(ErrorMessage = "The transfer description is required.")]
        [StringLength(50, ErrorMessage = "The description cannot exceed 50 characters.")]
        public required string Description { get; init; }
    }
}

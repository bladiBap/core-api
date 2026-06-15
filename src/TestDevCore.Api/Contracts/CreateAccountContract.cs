using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestDevCore.Api.Contracts
{
    public record CreateAccountContract
    {
        [JsonPropertyName("holder")]
        [Required(ErrorMessage = "Holder name is required.")]
        [StringLength(20, ErrorMessage = "Holder name cannot exceed 20 characters.")]
        public required string Holder { get; init; }

        [JsonPropertyName("balance")]
        [Required(ErrorMessage = "Balance is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Balance initial must be a non-negative value.")]
        public required decimal Balance { get; init; }

        [JsonPropertyName("currencyId")]
        [Required(ErrorMessage = "Currency is required")]
        public required Guid CurrencyId { get; init; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TestDevCore.Api.Contracts
{
    public record GetBalanceReportContract
    {
        [Required]
        public DateTime StartDate { get; init; }

        [Required]
        public DateTime EndDate { get; init; }

        [Required]
        public string Currency { get; init; } = string.Empty;
    }
}
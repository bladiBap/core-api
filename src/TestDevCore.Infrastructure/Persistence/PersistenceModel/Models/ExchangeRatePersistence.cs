using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestDevCore.Infrastructure.Persistence.PersistenceModel.Models
{
    [Table("ExchangeRates")]
    internal class ExchangeRatePersistence
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("SymbolBase")]
        [Required]
        [MaxLength(10)]
        public string SymbolBase { get; set; } = string.Empty;

        [Column("SymbolTarget")]
        [Required]
        [MaxLength(10)]
        public string SymbolTarget { get; set; } = string.Empty;

        [Column("Rate", TypeName = "decimal(18,4)")]
        [Required]
        public decimal Rate { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}

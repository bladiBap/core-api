using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDevCore.Infrastructure.Persistence.PersistenceModel.Models
{
    [Table("Accounts")]
    internal class AccountPersistence
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Holder")]
        [Required]
        [MaxLength(100)]
        public string Holder { get; set; } = string.Empty;

        [Column("Balance")]
        [Required]
        public decimal Balance { get; set; }

        [Column("IsActive")]
        [Required]
        public bool IsActive { get; set; }

        [Column("CurrencyId")]
        [Required]
        public Guid CurrencyId { get; set; }

        [ForeignKey(nameof(CurrencyId))]
        public virtual CurrencyPersistence Currency { get; set; } = null!;

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}

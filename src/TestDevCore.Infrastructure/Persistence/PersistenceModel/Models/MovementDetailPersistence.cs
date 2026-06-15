using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDevCore.Infrastructure.Persistence.PersistenceModel.Models
{
    [Table("MovementDetails")]
    internal class MovementDetailPersistence
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("MovementId")]
        [Required]
        public Guid MovementId { get; set; }

        [Column("AccountId")]
        [Required]
        public Guid AccountId { get; set; }

        [Column("Amount", TypeName = "decimal(18,2)")]
        [Required]
        public decimal Amount { get; set; }

        [Column("NewBalance", TypeName = "decimal(18,2)")]
        [Required]
        public decimal NewBalance { get; set; }

        [Column("PreviousBalance", TypeName = "decimal(18,2)")]
        [Required]
        public decimal PreviousBalance { get; set; }

        [ForeignKey(nameof(MovementId))]
        public virtual MovementPersistence Movement { get; set; } = null!;

        [ForeignKey(nameof(AccountId))]
        public virtual AccountPersistence Account { get; set; } = null!;

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}

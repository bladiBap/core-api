using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDevCore.Infrastructure.Persistence.PersistenceModel.Models
{
    [Table("Movements")]
    internal class MovementPersistence
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Description")]
        [Required]
        [MaxLength(30)]
        public string Description { get; set; } = string.Empty;

        [Column("ExchangeRate")]
        [Required]
        public decimal ExchangeRate { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [Column("Type")]
        [Required]
        public int Type { get; set; }

        public virtual ICollection<MovementDetailPersistence> MovementDetails { get; set; } = new List<MovementDetailPersistence>();
    }
}

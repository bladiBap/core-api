using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDevCore.Infrastructure.Persistence.PersistenceModel.Models
{
    [Table("Currencies")]
    internal class CurrencyPersistence
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Symbol")]
        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; } = string.Empty;

        [Column("Name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<AccountPersistence> Accounts { get; set; } = new List<AccountPersistence>();
    }
}

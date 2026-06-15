using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestDevCore.Domain.Currencies.Entities;

namespace TestDevCore.Infrastructure.Persistence.DomainModel.Config
{
    internal class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Id");

            builder.Property(x => x.Symbol)
                .HasColumnName("Symbol")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("UpdatedAt");

            builder.HasIndex(x => x.Symbol).IsUnique();
        }
    }
}

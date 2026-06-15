using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Currencies.Entities;

namespace TestDevCore.Infrastructure.Persistence.DomainModel.Config
{
    internal class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                   .ValueGeneratedNever();

            
            builder.OwnsOne(a => a.Balance, balance =>
            {
                balance.Property(b => b.Value)
                       .HasColumnName("Balance")
                       .HasPrecision(18, 2)
                       .IsRequired();
            });

            builder.Property(a => a.Holder)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(a => a.CurrencyId)
                   .IsRequired();

            builder.Property(a => a.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(a => a.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(a => a.UpdatedAt)
                .HasColumnName("UpdatedAt");

            builder.HasOne<Currency>()
                   .WithMany()
                   .HasForeignKey(a => a.CurrencyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestDevCore.Domain.Accounts.Entities;
using TestDevCore.Domain.Movements.Entities;

namespace TestDevCore.Infrastructure.Persistence.DomainModel.Config
{
    internal class MovementDetailConfig : IEntityTypeConfiguration<MovementDetail>
    {
        public void Configure(EntityTypeBuilder<MovementDetail> builder)
        {
            builder.ToTable("MovementDetails");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedNever();

            builder.Property(x => x.MovementId)
            .HasColumnName("MovementId");

            builder.OwnsOne(x => x.Amount, amount =>
            {
                amount.Property(p => p.Value)
                    .HasColumnName("Amount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.NewBalance, newBalance =>
            {
                newBalance.Property(p => p.Value)
                    .HasColumnName("NewBalance")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.PreviousBalance, previousBalance =>
            {
                previousBalance.Property(p => p.Value)
                    .HasColumnName("PreviousBalance")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            builder.Property(d => d.CreatedAt).IsRequired();
            builder.Property(d => d.UpdatedAt).IsRequired(false);

            builder.HasOne<Account>()
                .WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}

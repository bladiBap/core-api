using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Domain.Movements.Entities;
using TestDevCore.Infrastructure.Persistence.PersistenceModel.Models;

namespace TestDevCore.Infrastructure.Persistence.DomainModel.Config
{
    internal class MovementConfig : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {

            builder.ToTable("Movements");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();

            builder.Property(m => m.Description)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.OwnsOne(x => x.ExchangeRate, exchangeRate =>
            {
                exchangeRate.Property(p => p.Value)
                    .HasColumnName("ExchangeRate")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            builder.Property(m => m.Type)
                   .IsRequired();

            builder.Property(m => m.CreatedAt).IsRequired();
            builder.Property(m => m.UpdatedAt).IsRequired(false);

            builder.HasMany(m => m.MovementDetails)
                   .WithOne()
                   .HasForeignKey(d => d.MovementId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

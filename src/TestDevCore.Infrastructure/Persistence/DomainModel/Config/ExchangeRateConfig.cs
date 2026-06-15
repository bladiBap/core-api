using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Domain.ExchangeRates.Entities;
using TestDevCore.Infrastructure.Persistence.PersistenceModel.Models;

namespace TestDevCore.Infrastructure.Persistence.DomainModel.Config
{
    internal class ExchangeRateConfig : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {
            
            builder.ToTable("ExchangeRates");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.SymbolBase)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.SymbolTarget)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.Rate)
                   .HasColumnType("decimal(18,4)")
                   .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("UpdatedAt");


            builder.HasIndex(e => new { e.SymbolBase, e.SymbolTarget })
                   .IsUnique()
                   .HasDatabaseName("IX_ExchangeRates_SymbolBase_SymbolTarget");
        }
    }
}

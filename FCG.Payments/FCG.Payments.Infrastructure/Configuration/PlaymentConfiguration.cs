using FCG.Payments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Configuration
{
    public class PlaymentConfiguration : IEntityTypeConfiguration<Playment>
    {
        public void Configure(EntityTypeBuilder<Playment> builder)
        {
            builder.ToTable("TB_PAGAMENTO");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").HasColumnName("ISN_PAGAMENTO").UseIdentityColumn();
            builder.Property(p => p.DateCreation).HasColumnType("DATETIME").HasColumnName("DTH_CRIACAO").IsRequired();
            builder.Property(p => p.DateUpdate).HasColumnType("DATETIME").HasColumnName("DTH_ATUALIZACAO").IsRequired();
            builder.Property(p => p.MethodPayment).HasConversion<string>().HasColumnType("VARCHAR(1)").HasColumnName("COD_PAGAMENTO");
            builder.Property(p => p.StatusPayment).HasConversion<string>().HasColumnType("VARCHAR(1)").HasColumnName("COD_STATUS");
            builder.Property(p => p.UserId).HasColumnType("INT").HasColumnName("ISN_USUARIO");
            builder.Property(p => p.GameId).HasColumnType("INT").HasColumnName("ISN_GAME");

        }
    }
}

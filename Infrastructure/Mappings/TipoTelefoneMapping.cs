using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings;

public class TipoTelefoneMapping : IEntityTypeConfiguration<TipoTelefone>
{
    public void Configure(EntityTypeBuilder<TipoTelefone> builder)
    {
        builder.ToTable("TipoTelefone");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
    }
}

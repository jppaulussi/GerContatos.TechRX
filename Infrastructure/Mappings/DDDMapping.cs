using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings;

public class DDDMapping : IEntityTypeConfiguration<DDD>
{
    public void Configure(EntityTypeBuilder<DDD> builder)
    {
        builder.ToTable("DDD");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CodigoDDD)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(10);

        builder.HasOne(d => d.Regiao)
            .WithMany(r => r.DDDs)
            .HasForeignKey(d => d.RegiaoId);


    }
}

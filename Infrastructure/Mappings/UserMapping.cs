using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings;

public class UserMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {

        builder.ToTable("Usuario");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(150);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(150);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.RoleId)
            .IsRequired()
            .HasColumnType("INT");

        builder.HasOne(x => x.Role)
            .WithMany(c => c.Usuarios)
            .HasForeignKey(c => c.RoleId);
    }
}

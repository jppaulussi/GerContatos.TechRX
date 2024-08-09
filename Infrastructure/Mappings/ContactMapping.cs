using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings;

public class ContactMapping : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contato");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(150);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(150);

        builder.Property(x => x.Telefone)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(9);


        builder.HasOne(c => c.Regiao)
            .WithMany(r => r.Contatos)
            .HasForeignKey(c => c.RegiaoId);

        builder.HasOne(c => c.Usuario)
           .WithMany(u => u.Contatos)
           .HasForeignKey(c => c.UsuarioId);

        builder.HasOne(c => c.TipoTelefone)
           .WithMany(t => t.Contatos)
           .HasForeignKey(c => c.TipoTelefoneId);
    }
}

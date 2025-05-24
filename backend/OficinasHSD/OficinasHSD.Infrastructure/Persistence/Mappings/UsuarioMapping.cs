using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Infrastructure.Persistence.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("TbUsuario");

        builder.HasKey(p => p.Id);

        builder.HasIndex(u => u.Username)
                  .IsUnique()
                  .HasDatabaseName("IX_TbUsuario_Username");

        builder.Property(u => u.Username)
               .HasColumnType("varchar(100)")
               .IsRequired();

        builder.Property(u => u.PasswordHash)
               .HasColumnType("varchar(256)")
               .IsRequired();
    }
}
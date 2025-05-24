using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Infrastructure.Persistence.Mappings;

public class OficinaMecanicaMapping : IEntityTypeConfiguration<OficinaMecanica>
{
    public void Configure(EntityTypeBuilder<OficinaMecanica> builder)
    {
        builder.ToTable("TbOficinaMecanica");

        builder.HasKey(p => p.Id);

        builder.Property(o => o.Nome)
               .HasColumnType("nvarchar(200)")
               .IsRequired();

        builder.Property(o => o.Endereco)
               .HasColumnType("nvarchar(300)")
               .IsRequired();

        builder.Property(o => o.Servicos)
               .HasColumnType("nvarchar(200)")
               .IsRequired();

    }
}
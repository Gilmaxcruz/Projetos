using Mantimentos.App.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mantimentos.App.Data.Mappings
{
    /// <summary>
    /// Criado Class UnidadeMedidaMapping para Mapear conforme desejamos que seja criado o banco, dessa forma podemos manipular os dados a serem gerados,
    /// Tendo em vista que deixar que o migration crie a relação do banco não é algo funcional.
    /// </summary>
    class UnidadeMedidaMapping : IEntityTypeConfiguration<UnidadeMedida>
    {
        public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
        {
            builder.HasKey(c => c.Sigla);

            builder.Property(c => c.Unidade)
            .IsRequired()
            .HasColumnType("varchar(4)");

            builder.Property(c => c.Unidade)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.ToTable("UnidadeMedidas");

        }
    }
}


using Mantimentos.App.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mantimentos.App.Data.Mappings
{
    /// <summary>
    /// Criado Class MantimentoMapping para Mapear conforme desejamos que seja criado o banco, dessa forma podemos manipular os dados a serem gerados,
    /// Tendo em vista que deixar que o migration crie a relação do banco não é algo funcional.
    /// </summary>
    class MantimentoMapping : IEntityTypeConfiguration<Mantimento>
    {
        public void Configure(EntityTypeBuilder<Mantimento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Capacidade)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasColumnType("varchar(100)");


            builder.Property(c => c.ConteudoAtual)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("Mantimentos");

            // N : N MANTIMENTO : MOVIMENTO
            builder.HasMany(m => m.Movimentos)
                .WithMany(m => m.Mantimentos);

            // 1: N MARCA : MANTIMENTO
            builder.HasOne(m => m.Marca)
                .WithMany()
                .HasForeignKey(m =>m.MarcaId);

            // 1: N TPMANTMENTO : MANTIMENTO
            builder.HasOne(m => m.TpMantimento)
                .WithMany()
                .HasForeignKey(m => m.TipoMantimentoId);

            // 1: N UNIDADE : MANTIMENTO
            builder.HasOne(m => m.UnidadeMedida)
                .WithMany()
                .HasForeignKey(m => m.UnidadeSigla);



        }
    }
}

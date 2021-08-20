using Mantimentos.App.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mantimentos.App.Data.Mappings
{
    /// <summary>
    /// Criado Class TpMantimentoMapping para Mapear conforme desejamos que seja criado o banco, dessa forma podemos manipular os dados a serem gerados,
    /// Tendo em vista que deixar que o migration crie a relação do banco não é algo funcional.
    /// </summary>
    class TpMantimentoMapping : IEntityTypeConfiguration<TpMantimento>
    {
        public void Configure(EntityTypeBuilder<TpMantimento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.HasMany(m => m.TpMantimentoCategoria)
                 .WithOne(m => m.TpMantimento);
                 

            builder.ToTable("TpMantimentos");
        }
    }
}

using Mantimentos.App.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Mantimentos.App.Data.Mappings
{
    /// <summary>
    /// Criado Class CategoriaMapping para Mapear conforme desejamos que seja criado o banco, dessa forma podemos manipular os dados a serem gerados,
    /// Tendo em vista que deixar que o migration crie a relação do banco não é algo funcional.
    /// </summary>
    class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CategoriaNome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.HasMany(m => m.TpMantimentoCategoria)
                 .WithOne(m => m.Categoria);

            builder.ToTable("Categorias");



        }
    }
}


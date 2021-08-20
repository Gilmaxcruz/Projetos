using Mantimentos.App.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mantimentos.App.Data.Mappings
{
    /// <summary>
    /// Criado Class MovimentoMapping para Mapear conforme desejamos que seja criado o banco, dessa forma podemos manipular os dados a serem gerados,
    /// Tendo em vista que deixar que o migration crie a relação do banco não é algo funcional.
    /// </summary>
    class MovimentoMapping : IEntityTypeConfiguration<Movimento>
    {
        public void Configure(EntityTypeBuilder<Movimento> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.ToTable("Movimentos");

            // N : N MANTIMENTO : MOVIMENTO
            builder.HasMany(m => m.Mantimentos)
                .WithMany(m => m.Movimentos);
        }
    }
}


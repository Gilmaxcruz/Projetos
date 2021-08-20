using Mantimentos.App.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mantimentos.App.Data.Mappings
{
    /// <summary>
    /// Criado Class TpMantimentoCategoriaMapping para Mapear conforme desejamos que seja criado o banco, dessa forma podemos manipular os dados a serem gerados,
    /// Tendo em vista que deixar que o migration crie a relação do banco não é algo funcional.
    /// Esse tipo de tabela deve-se se atentar na criação  do registro para que no momento de criar uma lista, caso fosse de 1 para 1,
    /// Quebraria a ideia da tabela que foi contruida especificamente para conter o relacionamento da TPMantimentos e Categoria.
    /// </summary>
    class TpMantimentoCategoriaMapping : IEntityTypeConfiguration<TpMantimentoCategoria>
    {
        public void Configure(EntityTypeBuilder<TpMantimentoCategoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CategoriaId)
                .IsRequired();
                

            //1: n TPMANTMENTO : TpMantimentoCategoria
            builder.HasOne(m => m.TpMantimento)
                 .WithMany(m => m.TpMantimentoCategoria)
                 .HasForeignKey(m =>m.MantimentoTpId);




            //1: n Categoria : TpMantimentoCategoria
            builder.HasOne(m => m.Categoria)
                .WithMany()
                .HasForeignKey(m => m.CategoriaId);

            builder.ToTable("TpMantimentoCategorias");
        }
    }
}


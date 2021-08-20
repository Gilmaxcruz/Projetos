using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartFull.Business.Models;


namespace StartFull.Data.Mappings
{
    class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(200)"); // Microsoft.EntityFrameworkCore.Design

            builder.Property(p => p.Descricao)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

            builder.Property(p => p.Imagem)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            builder.ToTable("Produtos");
        }
    }
}

using Mantimentos.App.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Mantimentos.App.Data.Context
{
    /// <summary>
    /// Classe MantimentoDbContext responsavel pelo nosso Contexto;
    /// </summary>
    public class MantimentoDbContext : DbContext
    {
        public MantimentoDbContext(DbContextOptions<MantimentoDbContext> options) : base(options) {  
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Mantimento> Mantimentos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<TpMantimento> TipoMantimentos { get; set; }
        public DbSet<TpMantimentoCategoria> TpMantimentoCategorias { get; set; }
        public DbSet<UnidadeMedida> UnidadeMedidas { get; set; }

        /// <summary>
        /// Método OnModelCreating que carrega a instancia de um ModelBuilder.
        /// </summary>
        /// <param name="modelBuilder"> </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Nosso ModelBuilder tem por inclusão o foreach onde está sendo analisado uma parametrização onde quando estiver rodando o assembly,
            // fazendo sua criação do migration caso algum campo de tipo string não seja informado para que o mesmo recebe a criação varchar(100),
            // Tradando dessa forma não deixamos criar nvarchar(max).
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            //Definindo na criação que não será permitido a remoção casqueteavel no banco
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MantimentoDbContext).Assembly);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            base.OnModelCreating(modelBuilder);
        }
    }

}

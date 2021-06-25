using ApiPostgre.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPostgre.Data
{
    public class DataDbContext : DbContext
    {
        public DbSet<CadCli> CadClis { get; set; } 
        public DataDbContext(DbContextOptions<DataDbContext>options):
            base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CadCli>(
                    eb =>
                    {
                       // eb.HasNoKey();
                        eb.ToTable("cadcli");
                        eb.HasKey(v => v.recnum);
                        eb.Property(v => v.recnum).HasColumnName("recnum");
                        eb.Property(v => v.cliente).HasColumnName("cliente");
                        eb.Property(v => v.nome).HasColumnName("nome");
                        eb.Property(v => v.cpf).HasColumnName("cpf");

                    });
        }
    }
}

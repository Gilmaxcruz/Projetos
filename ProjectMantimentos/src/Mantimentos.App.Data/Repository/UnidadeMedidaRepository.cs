using Mantimentos.App.Business.Interfaces;
using Mantimentos.App.Business.Models;
using Mantimentos.App.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mantimentos.App.Data.Repository
{
    /// <summary>
    /// Repository de UnidadeMedidaRepository o mesmo foi necessario criar o Crud pois não Herda de Repositoy
    /// </summary>
    public class UnidadeMedidaRepository : IUnidadeMedidaRepository
    {
        protected readonly MantimentoDbContext Db;
        public UnidadeMedidaRepository(MantimentoDbContext db)
        {
            Db = db;
        }

        public async Task<UnidadeMedida> GetUnidadeID(string sigla)
        {
            UnidadeMedida siglas = await Db.UnidadeMedidas.Where(x => x.Sigla == sigla).FirstOrDefaultAsync();
            return siglas;
        }
        public void DeleteUnidade(string Sigla)
        {
            Db.UnidadeMedidas.Remove(Db.UnidadeMedidas.Where(x => x.Sigla == Sigla).FirstOrDefault());
            Db.SaveChanges();
        }

        public async Task<List<UnidadeMedida>> GetUnidade()
        {
            List<UnidadeMedida> lista = await Db.UnidadeMedidas.ToListAsync();
            return lista;
        }

        public void PostUnidade(UnidadeMedida UnidadeMedida)
        {
            Db.UnidadeMedidas.Add(UnidadeMedida);
            Db.SaveChanges();
        }

        public void PutUnidade(UnidadeMedida UnidadeMedida)
        {
            Db.Entry(UnidadeMedida).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}

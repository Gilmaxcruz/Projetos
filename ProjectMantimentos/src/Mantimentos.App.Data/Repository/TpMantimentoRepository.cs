using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mantimentos.App.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Mantimentos.App.Data.Repository
{
    /// <summary>
    /// Repository  TpMantimentoRepository não possui implementações alem das herdadas de Repository 
    /// </summary>
    public class TpMantimentoRepository : Repository<TpMantimento>, ITpMantimentoRepository
    {
        public TpMantimentoRepository(MantimentoDbContext context) : base(context) { }


        public void AdicionarTpMantimentoCategoria(TpMantimentoCategoria obj)
        {
            Db.TpMantimentoCategorias.Add(obj);
            Db.SaveChanges();

        }

        public void RemoverTpMantimentoCategoriaById(Guid tpMantimento)
        {
            List<TpMantimentoCategoria> mantimentos = Db.TpMantimentoCategorias
                .Where(x => x.MantimentoTpId == tpMantimento).ToList();

            Db.TpMantimentoCategorias.RemoveRange(mantimentos);
            Db.SaveChanges();
        }

        public virtual async Task<List<TpMantimento>> ObterTDados(TpMantimento obj)
        {
            IEnumerable<TpMantimento> mantimentos = Db.TipoMantimentos.AsNoTracking()
                .Include(f=>f.TpMantimentoCategoria)
              .AsEnumerable();
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj?.Nome))
                {
                    mantimentos = mantimentos.Where(x => x.Nome.ToUpper().Contains(obj.Nome.ToUpper())).AsEnumerable();
                }
            }
            return mantimentos.ToList();
        }
    }
}

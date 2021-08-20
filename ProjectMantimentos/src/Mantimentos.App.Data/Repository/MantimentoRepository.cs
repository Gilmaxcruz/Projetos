using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mantimentos.App.Data.Context;
using Mantimentos.App.Business.Enums;

namespace Mantimentos.App.Data.Repository
{
    public class MantimentoRepository : Repository<Mantimento>, IMantimentoRepository
    {
        public MantimentoRepository(MantimentoDbContext context) : base(context) { }


        public async Task<IEnumerable<Mantimento>> ObterMantimentoPorMarca(Guid Id)
        {
            return await Db.Mantimentos.AsNoTracking().Include(m => m.Marca).Where(m => m.MarcaId == Id)
                .OrderBy(p => p.Marca).ToListAsync();
        }
        public async Task<IEnumerable<Mantimento>> ObterMantimentoPorTipoMantimento(Guid Id)
        {
            return await Db.Mantimentos.AsNoTracking().Include(m => m.TpMantimento).Where(m => m.TipoMantimentoId == Id)
                .OrderBy(p => p.Marca).ToListAsync();
        }

        public async Task<IEnumerable<Mantimento>> ObterMantimentoMarca()
        {
            return await Db.Mantimentos.AsNoTracking().Include(f => f.Marca)
                .OrderBy(p => p.MarcaId).ToListAsync();
        }

        public async Task<IEnumerable<Mantimento>> ObterMantimentoTpMantimento()
        {
            return await Db.Mantimentos.AsNoTracking().Include(f => f.TpMantimento)
                .OrderBy(p => p.TipoMantimentoId).ToListAsync();
        }

        public async Task<IEnumerable<Mantimento>> ObterMantimentoUnidade()
        {
            return await Db.Mantimentos.AsNoTracking().Include(f => f.UnidadeMedida)
                .OrderBy(p => p.UnidadeSigla).ToListAsync();
        }

        public void AtualizarMantimentoPorId(Guid id, double qtd, TipoMovimento tipo)
        {
            Mantimento mantimento = Db.Mantimentos.Where(x => x.Id == id).FirstOrDefault();
            if (tipo == TipoMovimento.Entrada)
                mantimento.Estoque = mantimento.Estoque + qtd;
            else
                mantimento.Estoque = mantimento.Estoque - qtd;

            mantimento.ConteudoAtual = ((Convert.ToDouble(mantimento.Capacidade) * mantimento.Estoque) / 100).ToString();

            Db.Update(mantimento);
            Db.SaveChanges();
        }

        public virtual async Task<List<Mantimento>> ObterTDados(Mantimento obj)
        {
            IEnumerable<Mantimento> mantimentos = Db.Mantimentos.AsNoTracking()
               .Include(f => f.TpMantimento)
               .Include(f => f.Marca)
               .Include(f => f.UnidadeMedida)
              .AsEnumerable();
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj?.TpMantimento?.Nome))
                {
                    mantimentos = mantimentos.Where(x => x.TpMantimento.Nome.ToUpper().Contains (obj.TpMantimento.Nome.ToUpper())).AsEnumerable();
                }

                if (!string.IsNullOrWhiteSpace(obj?.Marca?.Nome))
                {
                    mantimentos = mantimentos.Where(x => x.Marca.Nome.ToUpper().Contains(obj.Marca.Nome.ToUpper())).AsEnumerable();
                }
                if (!string.IsNullOrWhiteSpace(obj?.UnidadeMedida?.Unidade))
                {
                    mantimentos = mantimentos.Where(x => x.UnidadeMedida.Unidade.ToUpper().Contains( obj.UnidadeMedida.Unidade.ToUpper())).AsEnumerable();
                }

            }
            return mantimentos.ToList();
        }


        public virtual async Task<List<Mantimento>> ObterNome()
        {

            return await DbSet.Include(f => f.TpMantimento).Include(f => f.Marca).ToListAsync();
        }


    }
}

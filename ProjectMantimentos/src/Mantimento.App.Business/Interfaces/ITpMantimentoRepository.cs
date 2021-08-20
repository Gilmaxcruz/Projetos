using Mantimentos.App.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mantimentos.App.Business.Interfaces
{
    /// <summary>
    /// Implementado a Interface de TpMantimento recebendo a interface gerenerica IRepositor.
    /// Sendo adicionado mais alguns metodos que será necessarios.
    /// </summary>
    public interface ITpMantimentoRepository : IRepository<TpMantimento>
    {
        public void AdicionarTpMantimentoCategoria(TpMantimentoCategoria obj);
        public void RemoverTpMantimentoCategoriaById(Guid tpMantimento);
        public Task<List<TpMantimento>> ObterTDados(TpMantimento tpMantimento);
    }
}

using Mantimentos.App.Business.Enums;
using Mantimentos.App.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mantimentos.App.Business.Interfaces
{
    /// <summary>
    ///  Implementado a Interface de Mantimento recebendo a interface gerenerica IRepository
    /// </summary>
    public interface IMantimentoRepository : IRepository<Mantimento>
    {
        Task<IEnumerable<Mantimento>> ObterMantimentoPorTipoMantimento(Guid Id);
        Task<IEnumerable<Mantimento>> ObterMantimentoPorMarca(Guid Id);

        Task<IEnumerable<Mantimento>> ObterMantimentoMarca();
        Task<IEnumerable<Mantimento>> ObterMantimentoTpMantimento();
        Task<IEnumerable<Mantimento>> ObterMantimentoUnidade();
        Task<List<Mantimento>> ObterTDados(Mantimento obj);

        void AtualizarMantimentoPorId(Guid id, double qtd, TipoMovimento tipo);
        public  Task<List<Mantimento>> ObterNome();
    }
}

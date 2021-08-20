using Mantimentos.App.Business.Models;
using System;


namespace Mantimentos.App.Business.Interfaces
{
    /// <summary>
    ///  Implementado a Interface de Movimento recebendo a interface gerenerica IRepository
    /// </summary>
    public interface IMovimentoRepository : IRepository<Movimento>
    {
    }
}

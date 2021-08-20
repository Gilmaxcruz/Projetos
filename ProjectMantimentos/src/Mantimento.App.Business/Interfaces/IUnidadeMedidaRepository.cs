using Mantimentos.App.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Mantimentos.App.Business.Interfaces
{
    /// <summary>
    /// Implementado a Interface Unidade, uma vez que ela não recebe os metodos genericos de IRepository,
    /// Foi necessario criar o Crud para a mesma.
    /// </summary>
    public interface IUnidadeMedidaRepository
    {

        Task<UnidadeMedida> GetUnidadeID(string Sigla);
        Task<List<UnidadeMedida>> GetUnidade();
        void PostUnidade(UnidadeMedida UnidadeMedida);
        void DeleteUnidade(string Sigla);

        void PutUnidade(UnidadeMedida UnidadeMedida);
    }
}

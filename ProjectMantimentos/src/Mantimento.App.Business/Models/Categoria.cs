
using System;
using System.Collections.Generic;

namespace Mantimentos.App.Business.Models
{
    /// <summary>
    /// Classe utilizada para criação da categoria, recebendo de Entity para geração de ID automatico.
    /// Utilizado List de TpMantimentoCategoria pois será gravado dados de relacionamento nessa tabela que possibilitara,
    /// uma melhor consulta ao TpMantimento.
    /// </summary>
    public class Categoria : Entity
    {
        public string CategoriaNome { get; set; }
        public List<TpMantimentoCategoria> TpMantimentoCategoria { get; set; }
    }
}

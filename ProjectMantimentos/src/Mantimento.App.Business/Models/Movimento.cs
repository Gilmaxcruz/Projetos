using Mantimentos.App.Business.Enums;
using System;
using System.Collections.Generic;

namespace Mantimentos.App.Business.Models
{
    /// <summary>
    /// Classe Movimento criada principalemente para que seja a fonte que alimentara o estoque dos mantimentos e será responsavel,
    /// por efetuar o calculo de porcentagem e de conteudo atual do mantimento a cada inclusão.
    /// </summary>
   public  class Movimento : Entity
    {   
        public Guid MantimentoId { get; set; }
        public double Quantidade { get; set; }
        public TipoMovimento TipoMovimento { get; set; }

        public ICollection<Mantimento> Mantimentos { get; set; }
    }

}

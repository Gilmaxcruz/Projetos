using System;
using System.Collections.Generic;

namespace Mantimentos.App.Business.Models
{
    public class TpMantimento : Entity
    {
        /// <summary>
        /// Classe TpMantimento, responsavel por conter o tipo de produto ex.:Leite a obrigatoriedade de necessecidade de reposição.
        /// </summary>
        public TpMantimento()
        {
            TpMantimentoCategoria = new();
        }
        public string Nome { get; set; }
        public bool Obrigatorio { get; set; }
        public List<TpMantimentoCategoria> TpMantimentoCategoria { get; set; }
    }
}

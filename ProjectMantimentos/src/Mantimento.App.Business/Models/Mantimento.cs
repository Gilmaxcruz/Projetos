using System;
using System.Collections.Generic;

namespace Mantimentos.App.Business.Models
{
    /// <summary>
    /// Classe Mantimento responsavel por conter o produto movimentado.
    /// criado campo não utilizado mas que necessitará ser implementado quando possivel, Imagem.
    /// </summary>
    public class Mantimento : Entity
    {
        public Guid TipoMantimentoId { get; set; }
        public Guid MarcaId { get; set; }
        public string UnidadeSigla { get; set; }
        public DateTime Validade { get; set; }
        public string Capacidade { get; set; }
        public string Imagem { get; set; }
        public string ConteudoAtual { get; set; }
        public double Estoque { get; set; }
        public double EstoqueMin { get; set; }
        public Marca Marca { get; set; }
        public TpMantimento TpMantimento { get; set; }
        public UnidadeMedida UnidadeMedida { get; set; }
        public ICollection<Movimento> Movimentos { get; set; }
    }
}

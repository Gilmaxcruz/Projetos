using System;
using System.Collections.Generic;


namespace Mantimentos.App.ViewModels
{
    public class MantimentoViewModel
    {
        public Guid Id { get; set; }
        public Guid TipoMantimentoId { get; set; }
        public Guid MarcaId { get; set; }
        public string UnidadeSigla { get; set; }
        public DateTime Validade { get; set; }
        public string Capacidade { get; set; }
      //  public IFormFile ImagemUp { get; set; }
        public string Imagem { get; set; }
        public string ConteudoAtual { get; set; }
        public double Estoque { get; set; }
        public double EstoqueMin { get; set; }
        public MarcaViewModel Marca { get; set; }
        public List<MarcaViewModel> Marcas { get; set; }
        public List<UnidadeMedidaViewModel> UnidadeMedidas { get; set; }
        public TpMantimentoViewModel TpMantimento { get; set; }
        public List<TpMantimentoViewModel> TpMantimentos { get; set; }
        public UnidadeMedidaViewModel UnidadeMedida { get; set; }
        public ICollection<MovimentoViewModel> Movimentos { get; set; }
    }
}

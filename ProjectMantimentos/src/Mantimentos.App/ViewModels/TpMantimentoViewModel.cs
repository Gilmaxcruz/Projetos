using System;
using System.Collections.Generic;

namespace Mantimentos.App.ViewModels
{
    public class TpMantimentoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Obrigatorio { get; set; }
        public CategoriaViewModel Categoria { get; set; }
        public List<CategoriaViewModel> Categorias { get; set; }
        public List<TpMantimentoCategoriaViewModel> TpMantimentoCategoriaViewModels { get; set; }
        public Guid[] CategoriaIds { get; set; }
    }
}

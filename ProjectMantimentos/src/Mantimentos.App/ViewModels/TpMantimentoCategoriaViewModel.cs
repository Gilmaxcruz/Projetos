using System;

namespace Mantimentos.App.ViewModels
{
    public class TpMantimentoCategoriaViewModel
    {
        public Guid Id { get; set; }
        public Guid MantimentoTpId { get; set; }
        public Guid CategoriaId { get; set; }
        public CategoriaViewModel Categoria { get; set; }
        public TpMantimentoViewModel TpMantimento { get; set; }
    }
}

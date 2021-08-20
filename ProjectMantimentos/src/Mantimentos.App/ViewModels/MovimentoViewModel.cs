using Mantimentos.App.Business.Enums;
using System;
using System.Collections.Generic;

namespace Mantimentos.App.ViewModels
{
    public class MovimentoViewModel
    {
        public Guid Id { get; set; }
        public Guid MantimentoId { get; set; }
        public double Quantidade { get; set; }
        public string Nome { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public List<MantimentoViewModel> Mantimentos { get; set; }
        public List<MovimentoSelectViewModel> MovimentoSelectViewModel { get; set; }
    }
}

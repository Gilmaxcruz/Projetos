using System;


namespace Mantimentos.App.Business.Models
{
    /// <summary>
    /// Classe TpMantimentoCategoria exclusiva para conter o vinculo de TpMantimento e Categoria.
    /// Não será necessario implementar CRUD a mesma.
    /// </summary>
    public class TpMantimentoCategoria : Entity
    {
        public Guid MantimentoTpId { get; set; }
        public Guid CategoriaId { get; set; }
        public TpMantimento TpMantimento { get; set; }
        public Categoria Categoria { get; set; }
    }
}

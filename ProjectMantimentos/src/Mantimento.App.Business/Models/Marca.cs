
namespace Mantimentos.App.Business.Models
{
    /// <summary>
    /// Classe responsavel por conter as diversas Marcas utilizadas no mantimento. 
    /// Possuindo apenas Nome e ID gerado de Entity.
    /// </summary>
    public class Marca : Entity
    {
        public string Nome { get; set; }
    }
}

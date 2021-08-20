
namespace Mantimentos.App.Business.Models
{
    public class UnidadeMedida 
    {
        /// <summary>
        /// Classe UnidadeMedida, criada para conter os tipos de Medida que o usuario possa desejar.
        /// Para essa utilizar não conter quebra não é interessante que sejá possivel fazer alteração da Sigla que é sua PK.
        /// Uma vez que se alterado de KG para grama é necessario recalcular o estoque. Para tal necessidade pode ser implementada futuramente.
        /// </summary>
        public string Unidade { get; set; }
        public string Sigla { get; set; }
    }
}

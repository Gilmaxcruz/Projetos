using FluentValidation;
using Mantimentos.App.ViewModels;

namespace Mantimentos.App.Validator
{
    /// <summary>
    /// Class CategoriaValidator responsavel por conter as validações parametrizando as possibilidades mantendo a integridade durante o processo.
    /// Funcionará apenas para o Edit pois Não é possivel utilizar no modal sem javaScript
    /// </summary>
  public  class UnidadeValidator : AbstractValidator<UnidadeMedidaViewModel>
    {
        public UnidadeValidator()
        {
            RuleFor(u => u.Sigla)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(1, 5).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e{MaxLength} caracteres");

            RuleFor(u => u.Unidade)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(1, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e{MaxLength} caracteres");
        }
    }
}

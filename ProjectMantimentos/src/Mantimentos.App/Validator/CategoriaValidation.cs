using FluentValidation;
using Mantimentos.App.ViewModels;

namespace Mantimentos.App.Validator
{
    /// <summary>
    /// Class CategoriaValidator responsavel por conter as validações parametrizando as possibilidades mantendo a integridade durante o processo.
    /// Funcionará apenas para o Edit pois Não é possivel utilizar no modal sem javaScrip
    /// </summary>
    public class CategoriaValidator : AbstractValidator<CategoriaViewModel>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.CategoriaNome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e{MaxLength} caracteres");
        }
    }
}

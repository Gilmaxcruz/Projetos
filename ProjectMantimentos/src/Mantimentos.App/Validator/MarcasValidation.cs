using FluentValidation;
using Mantimentos.App.ViewModels;

namespace Mantimentos.App.Validator
{
    /// <summary>
    /// Class MarcasValidator responsavel por conter as validações parametrizando as possibilidades mantendo a integridade durante o processo.
    /// Funcionará apenas para o Edit pois Não é possivel utilizar no modal sem javaScrip
    /// </summary>
    class MarcasValidator : AbstractValidator<MarcaViewModel>
    {
        public MarcasValidator()
        {
            RuleFor(m => m.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e{MaxLength} caracteres");
        }
    }
}

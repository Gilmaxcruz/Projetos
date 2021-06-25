using ApiPostgre.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiPostgre.Validators
{
    public class CadCliValidator : AbstractValidator<CadCli>
    {
        public CadCliValidator()
        {
          //  RuleFor(x => x.cliente).NotEmpty();//Não pode ser vazio nem nulo
            RuleFor(x => x.nome).MaximumLength(999999);//Maximo de digitos possivel
            RuleFor(x => x.cpf).Must(CpfValid).WithMessage("CPF/CNPJ INVALIDO!");
        }
        public bool CpfValid(string cpf)
        {
            Regex reg = new Regex(@"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");
            if (!reg.IsMatch(cpf))
                return false;
            else return true;
        }
    }
}

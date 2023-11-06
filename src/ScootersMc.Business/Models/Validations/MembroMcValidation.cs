using FluentValidation;
using ScootersMc.Business.Models.Validations.DocumentoValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Models.Validations
{
    public class MembroMcValidation : AbstractValidator<MembroMc>
    {
        public MembroMcValidation()
        {
            RuleFor(m => m.Nome)
                .NotEmpty().WithMessage("O campo {propertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {propertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(m => m.Cpf.Length).Equal(CpfValidacao.TamanhoCpf)
                .WithMessage("O campo Documento precisa ter {CompariasonValue} caracteres e foi {PropertyValue}.");
            RuleFor(m => CpfValidacao.Validar(m.Cpf)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");

            // Adicionar as validações dos campos que fazem parte da regra de negocio.
        }
    }
}

using AnimesControl.Application.Models.InputModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Application.Validations
{
    public class CustomerValidator : AbstractValidator<CustomerInputModel>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Usuário Inválido");
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email Inválido");
            RuleFor(x => x.Birthdate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data de Nascimento Inválida");
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25)
                .WithMessage("Senha Inválida");
            RuleFor(x => x.Name)
                 .NotNull()
                .NotEmpty()
                .MaximumLength(25)
                .WithMessage("Nome Inválido");

        }
    }
}

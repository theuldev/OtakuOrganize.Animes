using AnimesControl.Application.Models.InputModels;
using FluentValidation;
using Microsoft.AspNetCore.Rewrite;
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

            RuleFor(x => x.Birthdate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data de Nascimento Inválida");

            RuleFor(x => x.Name)
                 .NotNull()
                .NotEmpty()
                .MaximumLength(25)
                .WithMessage("Nome Inválido");

            RuleFor(x => x.Phone)
                .Length(11)
                .NotNull()
                .NotEmpty()
                .WithMessage("Telefone Inválido");
            RuleFor(x => x.LastName)
              .NotNull()
             .NotEmpty()
             .MaximumLength(25)
             .WithMessage("Sobrenome Inválido");

        }
    }
}

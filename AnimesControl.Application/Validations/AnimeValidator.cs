using AnimesControl.Application.Models.InputModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Application.Validations
{
    public class AnimeValidator : AbstractValidator<AnimeInputModel>
    {
        public AnimeValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20)
                .WithErrorCode("Titulo Inválido");
            RuleFor(x => x.Details)
               .NotEmpty()
               .NotNull()
               .MaximumLength(50)
               .WithErrorCode("Detalhes Inválido");
            RuleFor(x => x.PostAt)
                .NotNull()
                .NotEmpty()
                .WithMessage("Data de Postagem inválida");
            RuleFor(x => x.ReleaseDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Data de Lançamento Inválida");
            RuleFor(x => x.Category)
                .NotEmpty()
                .NotNull()
                .LessThanOrEqualTo(10)
                .WithMessage("Categoria Inválida");
        }
    }
}

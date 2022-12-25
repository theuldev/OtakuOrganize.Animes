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
    public class Anime_CustomerValidator: AbstractValidator<Anime_CustomerInputModel>
    {
        public Anime_CustomerValidator()
        {
            RuleFor(x => x.AnimeId)
                .NotNull()
                .NotEmpty()
                .WithMessage("O id do Anime está invalido");

            RuleFor(x => x.CustomerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("O id do Cliente está invalido");
        }
    }
}

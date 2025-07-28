using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Awards.CelebarityAwards
{
    public class CreateCelebirtyAwardDTOValidator : AbstractValidator<CreateCelebirtyAwardDTO>
    {
        public CreateCelebirtyAwardDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Award Name is required.")
                .MaximumLength(255);

            RuleFor(x => x.Poster)
                .NotNull().WithMessage("Poster is required.");

            RuleFor(x => x.AwardedDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Awarded date cannot be in the future.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(255);

            RuleFor(x => x.Organization)
                .NotEmpty().WithMessage("Organization is required.")
                .MaximumLength(255);
        }
    }
}

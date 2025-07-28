using EyeScenceApp.Application.DTOs.Awards.DigitalContentAwards;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Awards.DigitalContentsAwards
{
    public class UpdateDigitalContentAwardDTOValidator : AbstractValidator<UpdateDigitalContentAwardDTO>
    {
        public UpdateDigitalContentAwardDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Award ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Award name is required.")
                .MaximumLength(255);


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

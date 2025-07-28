using EyeScenceApp.Application.DTOs.Rates;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Rates
{
    public class CreateRateDTOValidation : AbstractValidator<CreateRateDTO>
    {
        public CreateRateDTOValidation() { 
         
            RuleFor(rate => rate.Value)
                .NotEmpty().WithMessage("Value is required.")
                .InclusiveBetween(0, 5).WithMessage("Value must be between 0 and 5.");
            RuleFor(rate => rate.Review)
                .MaximumLength(500).WithMessage("Review cannot exceed 500 characters.");
            RuleFor(rate => rate.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(rate => rate.DigitalContentId)
                .NotEmpty().WithMessage("DigitalContentId is required.")
                .Must(id => id != Guid.Empty).WithMessage("DigitalContentId cannot be an empty GUID.");
        }
    }
}

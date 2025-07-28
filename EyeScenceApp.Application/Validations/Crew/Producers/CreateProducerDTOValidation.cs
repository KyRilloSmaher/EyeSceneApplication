
using EyeScenceApp.Application.DTOs.Crew.Producers;
using EyeScenceApp.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Crews.Producers
{
    public class CreateProducerDTOValidation : AbstractValidator<CreateProducerDTO>
    {
        public CreateProducerDTOValidation() { 
          
            RuleFor(Producer => Producer.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(Producer => Producer.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(Producer => Producer.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");

            RuleFor(Producer => Producer.BirthDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Birth date cannot be in the future.");
            RuleFor(Producer => Producer.PrimaryImage)
                 .Must(image => image != null)
                 .WithMessage("You Should Upload A Primary Image For The Producer");

            RuleFor(Producer => Producer.Images)
                  .Must(images => images != null && images.Count() <= 10)
                  .WithMessage("You can upload a maximum of 10 images.");
            RuleFor(Producer => Producer.Sex)
                .IsInEnum()
                .WithMessage("Gender Must be Valid Type And Not Null");
            RuleFor(Producer => Producer.Sex)
                .IsInEnum()
                .WithMessage("Acting Style Must be Valid Type And Not Null");
            RuleFor(Producer => Producer.TotalBoxOfficeRevenue)
                .NotNull().WithMessage("TotalBoxOfficeRevenue cannot be null.");
            RuleFor(Producer=>Producer.Nationality)
                .IsInEnum()
                .WithMessage("Nationality Must be Valid Type And Not Null");


        }
    }
}

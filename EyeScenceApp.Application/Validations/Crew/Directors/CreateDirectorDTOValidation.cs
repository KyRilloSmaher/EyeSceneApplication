
using EyeScenceApp.Application.DTOs.Crew.Directors;
using EyeScenceApp.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Crews.Directors
{
    public class CreateDirectorDTOValidation : AbstractValidator<CreateDirectorDTO>
    {
        public CreateDirectorDTOValidation() { 
          
            RuleFor(director => director.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(director => director.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(director => director.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");

            RuleFor(director => director.BirthDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Birth date cannot be in the future.");

            RuleFor(Producer => Producer.PrimaryImage)
              .Must(image => image != null)
              .WithMessage("You Should Upload A Primary Image For The Director");

            RuleFor(Producer => Producer.Images)
                  .Must(images => images != null && images.Count() <= 10)
                  .WithMessage("You can upload a maximum of 10 images.");
            RuleFor(director => director.Sex)
                .IsInEnum()
                .WithMessage("Gender Must be Valid Type And Not Null");
            RuleFor(director => director.Sex)
                .IsInEnum()
                .WithMessage("Acting Style Must be Valid Type And Not Null");
            RuleFor(director => director.VisionStatement)
                .NotNull().WithMessage("VisionStatement cannot be null.");
            RuleFor(director=>director.Nationality)
                .IsInEnum()
                .WithMessage("Nationality Must be Valid Type And Not Null");


        }
    }
}

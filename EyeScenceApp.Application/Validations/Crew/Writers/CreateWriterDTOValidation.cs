
using EyeScenceApp.Application.DTOs.Crew.Writers;
using EyeScenceApp.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Crews.Writers
{
    public class CreateWriterDTOValidation : AbstractValidator<CreateWriterDTO>
    {
        public CreateWriterDTOValidation() { 
          
            RuleFor(Writer => Writer.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(Writer => Writer.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(Writer => Writer.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");

            RuleFor(Writer => Writer.BirthDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Birth date cannot be in the future.");

            RuleFor(Producer => Producer.PrimaryImage)
                     .Must(image => image != null)
                     .WithMessage("You Should Upload A Primary Image For The Writer");

            RuleFor(Producer => Producer.Images)
                  .Must(images => images != null && images.Count() <= 10)
                  .WithMessage("You can upload a maximum of 10 images.");
            RuleFor(Writer => Writer.Sex)
                .IsInEnum()
                .WithMessage("Gender Must be Valid Type And Not Null");
            RuleFor(Writer => Writer.Sex)
                .IsInEnum()
                .WithMessage("Acting Style Must be Valid Type And Not Null");
            RuleFor(Writer => Writer.WritingStyle)
                .IsInEnum()
                .NotNull().WithMessage("WritingStyle cannot be null.");
            RuleFor(Writer=>Writer.Nationality)
                .IsInEnum()
                .WithMessage("Nationality Must be Valid Type And Not Null");


        }
    }
}


using EyeScenceApp.Application.DTOs.Crew.Editiors;
using EyeScenceApp.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Crews.Editors
{
    public class CreateEditorDTOValidation : AbstractValidator<CreateEditorDTO>
    {
        public CreateEditorDTOValidation() { 
          
            RuleFor(Editor => Editor.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(Editor => Editor.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(Editor => Editor.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");

            RuleFor(Editor => Editor.BirthDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Birth date cannot be in the future.");

            RuleFor(Producer => Producer.PrimaryImage)
            .Must(image => image != null)
            .WithMessage("You Should Upload A Primary Image For The Editor");

            RuleFor(Producer => Producer.Images)
                  .Must(images => images != null && images.Count() <= 10)
                  .WithMessage("You can upload a maximum of 10 images.");
            RuleFor(Editor => Editor.Sex)
                .IsInEnum()
                .WithMessage("Gender Must be Valid Type And Not Null");
            RuleFor(Editor => Editor.Sex)
                .IsInEnum()
                .WithMessage("Acting Style Must be Valid Type And Not Null");
            RuleFor(Editor => Editor.EditingTechniques)
                .NotNull().WithMessage("EditingTechniques cannot be null.");
            RuleFor(Editor=>Editor.Nationality)
                .IsInEnum()
                .WithMessage("Nationality Must be Valid Type And Not Null");


        }
    }
}

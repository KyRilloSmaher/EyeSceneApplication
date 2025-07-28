
using EyeScenceApp.Application.DTOs.Crew.SoundDesigners;
using EyeScenceApp.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Crews.SoundDesigners
{
    public class CreateSoundDesignerDTOValidation : AbstractValidator<CreateSoundDesignerDTO>
    {
        public CreateSoundDesignerDTOValidation() { 
          
            RuleFor(SoundDesigner => SoundDesigner.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(SoundDesigner => SoundDesigner.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(SoundDesigner => SoundDesigner.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");

            RuleFor(SoundDesigner => SoundDesigner.BirthDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Birth date cannot be in the future.");

            RuleFor(Producer => Producer.PrimaryImage)
                  .Must(image => image != null)
                  .WithMessage("You Should Upload A Primary Image For The Sound Designer");

            RuleFor(Producer => Producer.Images)
                  .Must(images => images != null && images.Count() <= 10)
                  .WithMessage("You can upload a maximum of 10 images.");
            RuleFor(SoundDesigner => SoundDesigner.Sex)
                .IsInEnum()
                .WithMessage("Gender Must be Valid Type And Not Null");
            RuleFor(SoundDesigner => SoundDesigner.Sex)
                .IsInEnum()
                .WithMessage("Acting Style Must be Valid Type And Not Null");
            RuleFor(SoundDesigner => SoundDesigner.KnownForSoundtracks)
                .NotNull().WithMessage("KnownForSoundtracks cannot be null.");
            RuleFor(SoundDesigner=>SoundDesigner.Nationality)
                .IsInEnum()
                .WithMessage("Nationality Must be Valid Type And Not Null");


        }
    }
}

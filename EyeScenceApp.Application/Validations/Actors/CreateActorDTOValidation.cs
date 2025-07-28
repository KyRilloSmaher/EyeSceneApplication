using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Actors
{
    public class CreateActorDTOValidation : AbstractValidator<CreateActorDTO>
    {
        public CreateActorDTOValidation() { 
          
            RuleFor(actor => actor.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(actor => actor.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(actor => actor.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");

            RuleFor(actor => actor.BirthDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Birth date cannot be in the future.");

            RuleFor(actor => actor.TotalMovies)
                .GreaterThanOrEqualTo(0).WithMessage("Total movies must be a non-negative number.");
            RuleFor(actor => actor.Images)
                  .Must(images => images != null && images.Count <= 10)
                  .WithMessage("You can upload a maximum of 10 images.");
            RuleFor(actor => actor.Sex)
                .IsInEnum()
                .WithMessage("Gender Must be Valid Type And Not Null");
            RuleFor(actor => actor.ActingStyle)
                .IsInEnum()
                .WithMessage("Acting Style Must be Valid Type And Not Null");
            RuleFor(actor =>actor.IsCurrentlyActive)
                .NotNull().WithMessage("IsCurrentlyActive cannot be null.")
                .Must(value => value == true || value == false)
                .WithMessage("IsCurrentlyActive must be either true or false.");
            RuleFor(actor=>actor.Nationality)
                .IsInEnum()
                .WithMessage("Nationality Must be Valid Type And Not Null");


        }
    }
}

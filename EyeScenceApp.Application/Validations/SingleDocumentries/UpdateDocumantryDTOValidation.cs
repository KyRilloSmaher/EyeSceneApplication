using EyeScenceApp.Application.DTOs.SingleDocumentries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.SingleDocumentries
{
    public class UpdateMovieDTOValidation : AbstractValidator<UpdateDocumantryDTO>
    {
        public UpdateMovieDTOValidation(){
            RuleFor(doc => doc.Id)
                 .NotEmpty().WithMessage("Documantry ID is required.")
                 .Must(id => id != Guid.Empty).WithMessage("Documantry ID cannot be an empty GUID.");

            RuleFor(doc => doc.Title)
                 .NotEmpty().WithMessage("Title is required.")
                 .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
            RuleFor(doc => doc.ShortDescription)
                .Length(0, 500).WithMessage("Short description cannot exceed 500 characters.")
                .When(doc => !string.IsNullOrEmpty(doc.ShortDescription));
            RuleFor(doc => doc.CountryOfOrigin)
             .IsInEnum().WithErrorCode("InvalidCountryOfOrigin");
            RuleFor(doc => doc.DurationByMinutes)
                .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes.")
                .LessThanOrEqualTo(300).WithMessage("Duration cannot exceed 300 minutes.");
            RuleFor(doc => doc.ReleaseYear)
                .InclusiveBetween(1900, DateTime.UtcNow.Year).WithMessage("Release year must be between 1900 and the current year.");
           



        }
    }
}

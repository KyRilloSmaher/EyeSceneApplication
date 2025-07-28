using EyeScenceApp.Application.DTOs.Movies;
using EyeScenceApp.Application.DTOs.SingleDocumentries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Movies
{
    public class UpdateMovieDTOValidation : AbstractValidator<UpdateMovieDTO>
    {
        public UpdateMovieDTOValidation(){
            RuleFor(Movie => Movie.Id)
                 .NotEmpty().WithMessage("Movie ID is required.")
                 .Must(id => id != Guid.Empty).WithMessage("Movie ID cannot be an empty GUID.");

            RuleFor(Movie => Movie.Title)
                 .NotEmpty().WithMessage("Title is required.")
                 .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
            RuleFor(Movie => Movie.ShortDescription)
                .Length(0, 500).WithMessage("Short description cannot exceed 500 characters.")
                .When(Movie => !string.IsNullOrEmpty(Movie.ShortDescription));
            RuleFor(Movie => Movie.CountryOfOrigin)
                .IsInEnum().WithErrorCode("InvalidCountryOfOrigin");
            RuleFor(Movie => Movie.DurationByMinutes)
                .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes.")
                .LessThanOrEqualTo(300).WithMessage("Duration cannot exceed 300 minutes.");
            RuleFor(Movie => Movie.ReleaseYear)
                .InclusiveBetween(1900, DateTime.UtcNow.Year).WithMessage("Release year must be between 1900 and the current year.");
           
            RuleFor(Movie => Movie.CountryOfOrigin)
                .IsInEnum().WithErrorCode("InvalidCountryOfOrigin");
            RuleFor(doc => doc.Revenues)
               .NotEmpty()
               .WithMessage("Revenues is required.")
               .Must(revenues => revenues >= 0)
               .WithMessage("Revenues must be a non-negative number or null.");




        }
    }
}

using EyeScenceApp.Application.DTOs.Movies;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Movies
{
    public class CreateMovieDTOValidation : AbstractValidator<CreateMovieDTO>
    {
        public CreateMovieDTOValidation() { 
        
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
            RuleFor(Movie => Movie.Poster)
                .NotEmpty()
                .WithMessage("Poster file is required.")
                .Must(file => file.Length > 0)
                .WithMessage("Poster file must not be empty.")
                .Must(file => file.ContentType == "image/jpeg" || file.ContentType == "image/png");
            RuleFor(Movie => Movie.Revenues)
                .NotEmpty()
                .WithMessage("Revenues is required.")
                .Must(revenues => revenues >= 0)
                .WithMessage("Revenues must be a non-negative number or null.");




        }
    }
}

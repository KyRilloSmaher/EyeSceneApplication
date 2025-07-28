using EyeScenceApp.Application.DTOs.Series;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Series
{
    public class UpdateSeriesDTOValidator : AbstractValidator<UpdateSeriesDTO>
    {
        public UpdateSeriesDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Series ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(3, 255).WithMessage("Title must be between 3 and 255 characters.");

            RuleFor(x => x.ShortDescription)
                .NotEmpty().WithMessage("Short description is required.")
                .Length(10, 2500).WithMessage("Description must be between 10 and 2500 characters.");

            RuleFor(x => x.CountryOfOrigin)
                .IsInEnum().WithMessage("Invalid country selection.");

            RuleFor(x => x.DurationByMinutes)
                .InclusiveBetween(30, 240).WithMessage("Duration must be between 30 and 240 minutes.");

            RuleFor(x => x.ReleaseYear)
                .InclusiveBetween(1900, 2025).WithMessage("Release year must be between 1900 and 2025.");

            RuleFor(x => x.SeasonsCount)
                .GreaterThan(0).WithMessage("Seasons count must be a positive number.");

            RuleFor(x => x.EpisodesCount)
                .GreaterThan(0).WithMessage("Episodes count must be a positive number.");
        }
    }
}

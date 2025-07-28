using EyeScenceApp.Application.DTOs.EpisodeDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Episodes
{
    public class UpdateEpisodeDTOValidator : AbstractValidator<UpdateEpisodeDTO>
    {
        public UpdateEpisodeDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Episode ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(3, 255).WithMessage("Title must be between 3 and 255 characters.");

            RuleFor(x => x.ShortDescription)
                .NotEmpty().WithMessage("Short description is required.")
                .Length(10, 2500).WithMessage("Description must be between 10 and 2500 characters.");

            RuleFor(x => x.DurationByMinutes)
                .InclusiveBetween(30, 240).WithMessage("Duration must be between 30 and 240 minutes.");

            RuleFor(x => x.Season)
                .GreaterThan(0).WithMessage("Season number must be a positive number.");

            RuleFor(x => x.EpisodeNumber)
                .GreaterThan(0).WithMessage("Episode number must be a positive number.");

            RuleFor(x => x.SeriesId)
                .NotEmpty().WithMessage("Series ID is required.");
        }
    }
}

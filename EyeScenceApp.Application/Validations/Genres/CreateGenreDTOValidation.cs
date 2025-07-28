using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.Genres;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Genres
{
    public class CreateGenreDTOValidation : AbstractValidator<CreateGenreDTO>
    {
        public CreateGenreDTOValidation() { 
            RuleFor(g=>g.Name).NotEmpty().MinimumLength(3).WithMessage("Genre Name can not be Empty or less than 3 char");
            RuleFor(g => g.Poster).NotEmpty().WithMessage("Genre Poster is Required");
        }
    }
}

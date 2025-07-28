using EyeScenceApp.Application.DTOs.Genres;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Genres
{
    public class UpdateGenreDTOValidation: AbstractValidator<UpdateGenreDTO>
    {
        public UpdateGenreDTOValidation() {
            RuleFor(g => g.Id).NotEmpty().WithMessage("Id is Required");
            RuleFor(g => g.Name).NotEmpty().MinimumLength(3).WithMessage("Genre Name can not be Empty or less than 3 char");
        }
    }
}

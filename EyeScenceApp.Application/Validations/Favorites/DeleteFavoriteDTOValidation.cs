using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.Favorites;
using EyeScenceApp.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Favorites
{
    public class DeleteFavoriteDTOValidation : AbstractValidator<DeleteDigitalContentToUserFavoriteListDTO>
    {
        public DeleteFavoriteDTOValidation() {

            RuleFor(dto => dto.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .MaximumLength(50).WithMessage("User ID cannot exceed 50 characters.");
            RuleFor(dto => dto.DigitalContentId)
                .NotEmpty().WithMessage("Digital Content ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Digital Content ID cannot be an empty GUID.");

        }
    }
}

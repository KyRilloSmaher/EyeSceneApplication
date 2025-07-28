using EyeScenceApp.Application.DTOs.ApplicationUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Users
{
    public class ConfirmResetPasswordCodeDTOValidator :AbstractValidator<ConfirmResetPasswordCodeDTO>

    {
        public ConfirmResetPasswordCodeDTOValidator() {
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();

            RuleFor(x => x.Code)
          .NotEmpty().WithMessage("code is required.")
          .MinimumLength(6).WithMessage("code must be at least 6 characters.");
        }
    }
}

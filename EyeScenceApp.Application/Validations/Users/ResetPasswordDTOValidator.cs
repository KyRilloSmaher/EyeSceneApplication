using EyeScenceApp.Application.DTOs.ApplicationUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Users
{
    public class ResetPasswordDTOValidator :AbstractValidator<ResetPasswordDTO>
    {
        public ResetPasswordDTOValidator() {

            RuleFor(x => x.Email)
                   .NotEmpty().WithMessage("Email is required.")
                   .EmailAddress();

            RuleFor(x => x.NewPassword)
                  .NotEmpty().WithMessage("Password is required.")
                  .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}

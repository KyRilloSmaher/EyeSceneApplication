using EyeScenceApp.Application.DTOs.ApplicationUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Users
{
    public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordDTOValidator() {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("OldPassword is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage("New Password is required.")
               .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}

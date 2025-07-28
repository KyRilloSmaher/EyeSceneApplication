using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Validations.Users
{
    public class ResetPasswordCodeDTOValidator:AbstractValidator<SendResetCodeCommandModel>
    {
        public ResetPasswordCodeDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();
        }
    }
}

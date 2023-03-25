using BasicApplication.Domain.Dtos;
using FluentValidation;
using System.Linq;

namespace BasicApplication.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreationDto>
    {
        public UserCreateValidator()
        {
            RuleFor(customer => customer.FirstName).NotEmpty().WithMessage("First name is a required Field");
            RuleFor(customer => customer.LastName).NotEmpty().WithMessage("Last name is a required Field");
            RuleFor(customer => customer.Email).NotEmpty().WithMessage("Email is a required Field");
            RuleFor(customer => customer.Email).EmailAddress().WithMessage("Email not valid");
            RuleFor(customer => customer.Password).NotEmpty().WithMessage("Password is a required Field");
            RuleFor(customer => customer.Password)
                            .Must(p => p != null && p.Length >= 8 && p.Any(char.IsDigit) && p.Any(char.IsUpper))
                            .WithMessage("Password must be at least 8 characters long and contain at least one digit and one uppercase letter");
        }
    }
}

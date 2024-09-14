using FlavorVerse.ViewModels;
using FluentValidation;

namespace FlavorVerse.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(_ => _.FirstName.Value)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(20)
                .WithMessage("First name must be at most 20 characters long.")
                .MinimumLength(2)
                .WithMessage("First name must be at least 2 characters long.");

            RuleFor(_ => _.LastName.Value)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(30)
                .WithMessage("Last name must be at most 30 characters long.")
                .MinimumLength(2)
                .WithMessage("Last name must be at least 2 characters long.");

            RuleFor(_ => _.Email.Value)
                   .NotEmpty()
                   .WithMessage("Email is required.")
                   .EmailAddress()
                   .WithMessage("Invalid email format.")
                   .MaximumLength(100)
                   .WithMessage("Email must be at most 100 characters long.");

            RuleFor(_ => _.Phone.Value)
                .NotEmpty()
                .WithMessage("Phone is required.")
                .Matches(@"^\d+$")
                .WithMessage("Phone number must contain only numbers.")
                .MaximumLength(15)
                .WithMessage("Phone number must be at most 15 digits long.")
                .MinimumLength(10)
                .WithMessage("Phone number must be at least 10 digits long.");

            RuleFor(_ => _.Password.Value)
                  .NotEmpty()
                  .WithMessage("Password is required.")
                  .MinimumLength(8)
                  .WithMessage("Password must be at least 8 characters long.")
                  .Matches("[A-Z]")
                  .WithMessage("Password must contain at least one uppercase letter.")
                  .Matches("[a-z]")
                  .WithMessage("Password must contain at least one lowercase letter.")
                  .Matches("[0-9]")
                  .WithMessage("Password must contain at least one digit.")
                  .Matches(@"[!@#$%^&*(),.?"":{ }|<>]")
                  .WithMessage("Password must contain at least one special character.");

            RuleFor(_ => _.ConfirmPassword.Value)
                .Equal(_ => _.Password.Value)
                .WithMessage("Passwords do not match.");

            RuleFor(_ => _.DateOfBirth.Value)
                .NotEmpty()
                .NotNull()
                .WithMessage("Date of birth is required.")
                .Must(Extensions.Extensions.BeInThePast)
                .WithMessage("Date of birth must be in the past.")
                .Must(Extensions.Extensions.BeAValidAge)
                .WithMessage("Date of birth is not valid. You must be at least 12yso and not older than 100yso");
        }
    }
}
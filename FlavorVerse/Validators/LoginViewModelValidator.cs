using FlavorVerse.ViewModels;
using FluentValidation;

namespace FlavorVerse.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(_ => _.Email.Value)
                   .NotEmpty()
                   .WithMessage("Email is required.")
                   .EmailAddress()
                   .WithMessage("Invalid email format.")
                   .MaximumLength(100)
                   .WithMessage("Email must be at most 100 characters long.");

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
        }
    }
}
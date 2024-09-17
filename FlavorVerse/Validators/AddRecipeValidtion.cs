using FlavorVerse.ViewModels;
using FluentValidation;

namespace FlavorVerse.Validators
{
    public class AddRecipeValidtion : AbstractValidator<NewRecipeViewModel>
    {
        public AddRecipeValidtion()
        {
            RuleFor(x => x.Title.Value)
                .NotEmpty()
                .WithMessage("Title property is required.")
                .MaximumLength(100)
                .WithMessage("Title can have at most 100 characters.");

            RuleFor(x => x.Image.Value)
                .NotNull()
                .WithMessage("Recipe picture is required.");

            RuleFor(x => x.Description.Value)
                .NotEmpty()
                .WithMessage("Description property is required.")
                .MaximumLength(500)
                .WithMessage("Description can have at most 500 characters.");

            RuleFor(x => x.CookingTime.Value)
                .NotEmpty()
                .WithMessage("Cooking time property is required.")
                .MaximumLength(20)
                .WithMessage("Cooking time can have at most 20 characters.");

            RuleFor(x => x.Servings.Value)
                .NotEmpty()
                .WithMessage("Servings is required.")
                .GreaterThan(0)
                .WithMessage("Servings must be a whole positive number.");

            RuleFor(x => x.Instructions.Value)
                .NotEmpty()
                .WithMessage("Instructions property is required.")
                .MaximumLength(1000)
                .WithMessage("Instructions can have at most 1000 characters.");

            RuleFor(x => x.SelectedMealType)
               .NotEmpty()
               .WithMessage("Meal type is required.");

            RuleFor(x => x.SelectedDifficultyCooking)
               .NotEmpty()
               .WithMessage("Difficulty level is required.");

            RuleFor(x => x.SelectedCuisine)
               .NotEmpty()
               .WithMessage("Cuisine is required.");

            RuleFor(x => x.SelectedCategory)
               .NotEmpty()
               .WithMessage("Category is required.");
        }
    }
}
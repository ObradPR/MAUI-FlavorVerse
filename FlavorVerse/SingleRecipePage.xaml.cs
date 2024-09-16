using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.ViewModels;
using System.Text.RegularExpressions;

namespace FlavorVerse;

public partial class SingleRecipePage : ContentPage
{
    public SingleRecipePage(RecipeDto recipe)
    {
        InitializeComponent();

        recipe.Instructions = Regex.Replace(recipe.Instructions, @"(?<!^)(\d+\.)", "\n$1").TrimStart('\n');

        var viewModel = new SingleRecipeViewModel(recipe);
        BindingContext = viewModel;
    }
}
using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common;
using FlavorVerse.Interfaces;
using FlavorVerse.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FlavorVerse.ViewModels;

public class HomeViewModel
{
    private readonly IRecipeService _recipeService;

    public ObservableCollection<RecipeDto> Recipes { get; set; } = [];

    public ICommand GoToRecipesPageCommand { get; }

    public HomeViewModel()
    {
        _recipeService = new RecipeService();

        GoToRecipesPageCommand = new Command(() =>
            MessagingCenter.Send(this, Constants.NAVIGATE_TO_RECIPE_PAGE));

        LoadRecipes();
    }

    private void LoadRecipes()
    {
        var recipes = _recipeService.GetList();

        Recipes = new ObservableCollection<RecipeDto>(recipes.Items.Take(4));
    }
}
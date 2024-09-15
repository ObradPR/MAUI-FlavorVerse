using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common;
using FlavorVerse.Common.Grid;
using FlavorVerse.Interfaces;
using FlavorVerse.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlavorVerse.ViewModels;

public class RecipeViewModel : INotifyPropertyChanged
{
    private readonly IRecipeService _recipeService;

    public MProp<string> Keyword { get; set; } = new MProp<string>();

    public ObservableCollection<RecipeDto> Recipes { get; set; } = [];

    public RecipeViewModel()
    {
        _recipeService = new RecipeService();

        LoadRecipes();

        Keyword.OnChange = LoadRecipes;
    }

    private void LoadRecipes()
    {
        var queryParams = new QueryParams
        {
            SearchTerm = Keyword.Value
        };

        var recipes = _recipeService.GetList(queryParams);
        Recipes.Clear();
        Recipes = new ObservableCollection<RecipeDto>(recipes.Items);
        OnPropertyChanged(nameof(Recipes));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
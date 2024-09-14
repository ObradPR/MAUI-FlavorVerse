using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FlavorVerse.ViewModels;

public class HomeViewModel
{
    public ObservableCollection<RecipeDto> Recipes { get; set; }

    public ICommand GoToRecipesPageCommand { get; }

    public HomeViewModel()
    {
        GoToRecipesPageCommand = new Command(() =>
            MessagingCenter.Send(this, Constants.NAVIGATE_TO_RECIPE_PAGE));
    }
}
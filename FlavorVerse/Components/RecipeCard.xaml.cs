using FlavorVerse.BusinessLogic.Dto;

namespace FlavorVerse.Components;

public partial class RecipeCard : ContentView
{
    public RecipeCard()
    {
        InitializeComponent();
    }

    private async void OnRecipeTapped(object sender, EventArgs e)
    {
        var selectedRecipe = this.BindingContext as RecipeDto;
        if (selectedRecipe != null)
        {
            await Navigation.PushAsync(new SingleRecipePage(selectedRecipe));
        }
    }
}
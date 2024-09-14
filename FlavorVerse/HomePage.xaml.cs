using FlavorVerse.Common;
using FlavorVerse.Extensions;
using FlavorVerse.ViewModels;

namespace FlavorVerse;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        SetupWelcomeMessage();

        MessagingCenter.Subscribe<HomeViewModel>(this, Constants.NAVIGATE_TO_RECIPE_PAGE, async (sender) =>
        {
            var tabbedPage = (TabbedPage)Application.Current.MainPage;
            var recipeTab = tabbedPage.Children.OfType<NavigationPage>()
                .FirstOrDefault(np => np.RootPage is RecipePage);

            if (recipeTab != null)
            {
                tabbedPage.CurrentPage = recipeTab;
                await recipeTab.Navigation.PushAsync(new RecipePage());
            }
        });
    }

    private void SetupWelcomeMessage()
    {
        var user = SecureStorage.Default.GetUser();

        if (user != null)
        {
            WelcomeUserLabel.Text = $"Hello {user.DisplayName}, welcome to FlavorVerse!";
            WelcomeUserLabel.IsVisible = true;
        }
    }
}
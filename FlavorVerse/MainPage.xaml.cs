using FlavorVerse.Extensions;

namespace FlavorVerse;

public partial class MainPage : TabbedPage
{
    public bool IsLogged { get; set; }

    public MainPage(bool resetApp = false)
    {
        InitializeComponent();

        if (resetApp)
            SecureStorage.Default.Remove("token");

        var user = SecureStorage.Default.GetUser();

        if (user != null)
            IsLogged = true;

        SetUpTabs();
    }

    private void SetUpTabs()
    {
        this.Children.Add(new NavigationPage(new HomePage()) { Title = "Home" });
        this.Children.Add(new NavigationPage(new RecipePage()) { Title = "Recipes" });

        if (!IsLogged)
        {
            this.Children.Add(new NavigationPage(new LoginPage()) { Title = "Login" });
            this.Children.Add(new NavigationPage(new RegisterPage()) { Title = "Register" });
        }
        else
        {
            this.Children.Add(new NavigationPage(new LogoutPage()) { Title = "Logout" });
        }
    }
}
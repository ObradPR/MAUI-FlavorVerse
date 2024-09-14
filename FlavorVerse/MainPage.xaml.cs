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
        this.Children.Add(new HomePage());

        if (!IsLogged)
        {
            this.Children.Add(new LoginPage());
            this.Children.Add(new RegisterPage());
        }
    }
}
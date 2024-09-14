namespace FlavorVerse;

public class LogoutPage : ContentPage
{
    public LogoutPage()
    {
        Title = "Logout";

        Appearing += (sender, e) => App.Current.MainPage = new MainPage(true);
    }
}
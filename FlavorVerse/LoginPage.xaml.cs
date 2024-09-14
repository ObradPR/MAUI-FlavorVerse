namespace FlavorVerse;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();

        ApplyMarginToElements(ParentLayout, 10);
    }

    private void ApplyMarginToElements(Layout layout, double bottomMargin)
    {
        foreach (var child in layout.Children)
            if (child is Entry entry)
                entry.Margin = new Thickness(0, 0, 0, bottomMargin);
            else if (child is Label label)
                label.Margin = new Thickness(0, 0, 0, bottomMargin * 1.5);
    }
}
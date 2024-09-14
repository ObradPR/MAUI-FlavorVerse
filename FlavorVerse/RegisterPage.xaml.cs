namespace FlavorVerse;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();

        ParentLayout.SizeChanged += OnLayoutSizeChanged;

        ApplyMarginToElements(ParentLayout, 10);
    }

    private void OnLayoutSizeChanged(object sender, EventArgs e)
    {
        // Calculate effective width by subtracting the left and right padding
        double padding = ParentLayout.Padding.Left + ParentLayout.Padding.Right;
        double effectiveWidth = ParentLayout.Width - padding;

        // Set the DatePicker's WidthRequest to the effective width
        DatePicker.WidthRequest = effectiveWidth;
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
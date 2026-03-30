namespace TARpe24_Mobiilirakendused;

public partial class Pop_Up_Page : ContentPage
{
	public Pop_Up_Page()
	{
		Button alertButton = new Button
		{
			Text = "Teade",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
		alertButton.Clicked += AlertButton_Clicked;

		Button alertYesNoButton = new Button
		{
			Text = "Jah või ei",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
		alertYesNoButton.Clicked += alertYesNoButton_Clicked;

		Button alertListButton = new Button
		{
			Text = "Valik",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
        alertYesNoButton.Clicked += alertYesNoButton_Clicked;

		Content = new VerticalStackLayout
		{
			Spacing = 20,
			Padding = new Thickness(0, 50, 0, 0),
			Children = { alertListButton, alertYesNoButton, alertListButton }
		};

		
    }
    private async void AlertButton_Clicked(object? sender, EventArgs e)
    {
        await DisplayAlertAsync("Teade", "Teil on uus teade", "OK");

        await DisplayAlertAsync("Teade", "Teie valik on: " + (result ? "Jah" : "Ei"), "OK");
    }
    private async void AlertListButton_Clicked(object? sender, EventArgs e)
    {

    }
}

using System.Reflection;

namespace TARpe24_Mobiilirakendused;

public partial class LumememmPage : ContentPage
{
    // genereerimine
    Random rnd = new Random();

    //lumememme laadimine ekraanile
    public LumememmPage()
    {
        InitializeComponent();
    }
    //lAEB XAML UI
    private async void OnActionClicked(object sender, EventArgs e)
    {
        //Kontrollitakse kas midagi on valitud
        if (ActionPicker.SelectedItem == null)
            return;

        string action = ActionPicker.SelectedItem.ToString();
        ResultLabel.Text = "Valitud tegevus: " + action;

        uint speed = (uint)SpeedStepper.Value;

        switch (action)
        {
            // peidab kõik lumememme osad
            case "Peida":
                Body.IsVisible = false;
                Middle.IsVisible = false;
                Head.IsVisible = false;
                Hat.IsVisible = false;
                break;

            // teeb kõik nähtavaks
            case "Näita":
                Body.IsVisible = true;
                Middle.IsVisible = true;
                Head.IsVisible = true;
                Hat.IsVisible = true;

                // taastab läbipaistvuse
                Body.Opacity = 1;
                Middle.Opacity = 1;
                Head.Opacity = 1;
                Hat.Opacity = 1;
                break;

            // küsib kasutajalt kinnitust
            case "Muuda värvi":
                bool confirm = await DisplayAlert("Värv",
                    "Kas muuta lumememme värvi?",
                    "Jah", "Ei");

                if (confirm)
                {
                    // genereerib juhusliku värvi
                    Color randomColor = Color.FromRgb(
                        rnd.Next(256),
                        rnd.Next(256),
                        rnd.Next(256));

                    // muudab pallide värvi
                    Body.BackgroundColor = randomColor;
                    Middle.BackgroundColor = randomColor;
                    Head.BackgroundColor = randomColor;
                }
                break;

            case "Sulata":
                // teeb väiksemaks
                await Body.ScaleTo(0.5, speed);
                await Middle.ScaleTo(0.5, speed);
                await Head.ScaleTo(0.5, speed);
                await Hat.ScaleTo(0.5, speed);

                // muudab nähtamatuks (kaob ära)
                await Body.FadeTo(0, speed);
                await Middle.FadeTo(0, speed);
                await Head.FadeTo(0, speed);
                await Hat.FadeTo(0, speed);
                break;

            case "Tantsi":
                for (int i = 0; i < 3; i++)// kordab 3 korda 
                {
                    // hüppa üles
                    await Task.WhenAll(
                        Body.TranslateTo(0, -40, speed),
                        Middle.TranslateTo(0, -40, speed),
                        Head.TranslateTo(0, -40, speed),
                        Hat.TranslateTo(0, -40, speed)
                    );

                    // pööra natuke
                    await Task.WhenAll(
                        Body.RotateTo(10, speed),
                        Middle.RotateTo(-10, speed),
                        Head.RotateTo(10, speed),
                        Hat.RotateTo(10, speed)
                    );

                    // hüppa alla
                    await Task.WhenAll(
                        Body.TranslateTo(0, 0, speed),
                        Middle.TranslateTo(0, 0, speed),
                        Head.TranslateTo(0, 0, speed),
                        Hat.TranslateTo(0, 0, speed)
                    );

                    // pööra teisele poole
                    await Task.WhenAll(
                        Body.RotateTo(-10, speed),
                        Middle.RotateTo(10, speed),
                        Head.RotateTo(-10, speed),
                        Hat.RotateTo(-10, speed)
                    );
                }

                // reset
                await Task.WhenAll(
                    Body.RotateTo(0),
                    Middle.RotateTo(0),
                    Head.RotateTo(0),
                    Hat.RotateTo(0)
                );
                break;
        }
    }

    private void OnSliderChanged(object sender, ValueChangedEventArgs e)
    {
        Body.Opacity = e.NewValue;
        Middle.Opacity = e.NewValue;
        Head.Opacity = e.NewValue;
        Hat.Opacity = e.NewValue;
    }
}
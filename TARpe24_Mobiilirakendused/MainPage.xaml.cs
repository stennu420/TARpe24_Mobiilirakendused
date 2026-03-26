using Microsoft.Maui.Graphics.Text;
using System.Net.Http.Json;

namespace TARpe24_Mobiilirakendused
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";
            BotImage.Rotation += 20;
            var rnd = new Random();
            var rndColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            BackgroundColor = rndColor;

            if (count >= 10)
            {
                BotImage.IsVisible = false;
                CounterLabel.Text = "Pilt kadus ära! Vajuta Reset.";
            }
            if (count >= 5)
            {
                CounterBtn.BackgroundColor = Colors.Red;
                CounterBtn.TextColor = Colors.White;
            }



            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void ResetBtn_Clicked(object sender, EventArgs e)
        {
            count = 0;
            CounterBtn.Text = "Alustame uuesti!";
            BotImage.Rotation = 0;
            BotImage.IsVisible = true; // Toob pildi tagasi
                                       // Liigutame pildi paremasse serva
            BotImage.HorizontalOptions = LayoutOptions.End;

            // VÕI teeme loogika: kui on vasakul, liiguta paremale, ja vastupidi
            if (BotImage.HorizontalOptions == LayoutOptions.Start)
            {
                BotImage.HorizontalOptions = LayoutOptions.End;
            }
            else
            {
                BotImage.HorizontalOptions = LayoutOptions.Start;
            }

        }
    }
}

namespace TARpe24_Mobiilirakendused;

public partial class StartPage : ContentPage
{
    public List<ContentPage> Lehed = new List<ContentPage>() { new TextPage(), new Pop_Up_Page(), new FigurePage(), new ValgusfoorPage(), new DataTimePage(), new StepperSliderPage(), new SliderRgbPage(), new LumememmPage() , new Trips_Traps_Trull(), new KontaktPage(), new Elemendid_Page()};
    public List<string> LeheNimed = new List<string> { "TextPage", "Pop_Up_Page","FigurePage", "ValgusfoorPage", "DateTimePage", "StepperSliderPage", "SliderRgbPage", "Lumememmpage" , "Trips-Traps-Trull", "Kontakt", "Elemendid"};

    ScrollView sv;
    VerticalStackLayout vst;
    public StartPage()
    {

        Title = "Avaleht";
        vst = new VerticalStackLayout { Padding=20, Spacing=15 };
        for (int i = 0; i < Lehed.Count; i++)
        {
            Button nupp = new Button
            {
                Text = LeheNimed[i],
                FontSize = 36,
                FontFamily="Luffio",
                BackgroundColor = Colors.LightGray,
                TextColor = Colors.Black,
                CornerRadius = 10,
                HeightRequest = 60,
                ZIndex = i
            };
            vst.Add(nupp);
            nupp.Clicked += (sender, e) =>
            {
                var valik = Lehed[nupp.ZIndex];
                Navigation.PushAsync(valik);
            };
        }

        Button nulliNupp = new Button
        {
            Text = "Nulli seaded (Testsimiseks)",
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            Margin = new Thickness(0, 30, 0, 0)
        };
        nulliNupp.Clicked += async (sender, e) =>
        {
            Preferences.Default.Remove("EsimeneKäivitamine");
            await DisplayAlertAsync("Edukalt nullitud", "Mälu on tühjendatud. Kui sa lehe uuesti avad, käivitub äpp nagu täiesti uus!", "OK");
        };
        vst.Add(nulliNupp);
        sv = new ScrollView { Content = vst };
        Content = sv;
    }

    

    protected override async void OnAppearing() 
    {
        base.OnAppearing();

        bool onEsimeneStart = Preferences.Default.Get("EsimeneKäivitamine", true);

        if (onEsimeneStart) 
        {
            bool vastus = await DisplayAlertAsync("Tere tulemast!",
                "Tundub, et avastasid selle rakenduse esimest korda. Kas soovid näha lühikest juhendit?",
                "Jah, palun",
                "Ei, saan ise hakkama");

            if (vastus) 
            {
                await DisplayAlertAsync("Juhend",
                    "Siin on sinu lühike juhend: vali menüüst sobiv teem ja uuri, kuidas elemendid töötavad!",
                    "Selge");
            }
            Preferences.Default.Set("EsimeneKäivitamine", false);
        }

    }
  
}
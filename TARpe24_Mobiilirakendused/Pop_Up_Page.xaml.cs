namespace TARpe24_Mobiilirakendused;

public partial class Pop_Up_Page : ContentPage
{
    Random rnd = new Random(); // Juhuslike arvude generaator mõistatuste valimiseks

    // Mõistatuste ja vastuste nimekiri
    List<(string kysimus, string vastus)> moistatused = new List<(string, string)>()
    {
        ("Mis on see, mis jookseb, aga ei liigu?", "vesi"), // mõistatus 1
        ("Mis on see, millel on hambad, aga ei hammusta?", "kamm"), // mõistatus 2
        ("Mis tõuseb, aga ei lange kunagi?", "vanus"), // mõistatus 3
        ("Mis on see, mis on täis auke, aga hoiab vett?", "seep"), // mõistatus 4
        ("Mis liigub üles, kuid ei lange kunagi?", "temperatuur"), // mõistatus 5
        ("Mis on sul alati ees, aga ei näe seda?", "nina"), // mõistatus 6
        ("Mis on kergem kui sulg, aga isegi tuul ei kanna seda kaugele?", "hingamine"), // mõistatus 7
        ("Mis läheb üles, alla, vasakule ja paremale, aga ei liigu tegelikult?", "tee") // mõistatus 8
    };

    
    public Pop_Up_Page()
    {
        InitializeComponent();

        // Nupp mõistatuse kuvamiseks
        Button moistatusButton = new Button
        {
            Text = "Mõistatus",
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center
        };
        moistatusButton.Clicked += MoistatusButton_Clicked; //Käivitab mõistatuse

        // Nime sisestamine
        Button nimiButton = new Button
        {
            Text = "Sisesta nimi",
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center
        };
        nimiButton.Clicked += NimiButton_Clicked; 

        // Valiku tegemine
        Button valikButton = new Button
        {
            Text = "Valik",
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center
        };
        valikButton.Clicked += ValikButton_Clicked; 

       
        Content = new VerticalStackLayout
        {
            Spacing = 20, 
            Padding = new Thickness(20, 50), 
            Children = { moistatusButton, nimiButton, valikButton } 
        };
    }

    // Mõistatuse küsimise meetod
    private async void MoistatusButton_Clicked(object sender, EventArgs e)
    {
        var m = moistatused[rnd.Next(moistatused.Count)]; // Vali juhuslik mõistatus
        string vastus = await DisplayPromptAsync("Mõistatus", m.kysimus); // küsimine

        if (vastus != null && vastus.ToLower() == m.vastus)
        {
            await DisplayAlert("Õige!", "Tubli ", "OK"); 
        }
        else
        {
            await DisplayAlert("Vale!", $"Õige vastus on: {m.vastus}", "OK"); 
        }
    }

    // Nime küsimise meetod
    private async void NimiButton_Clicked(object sender, EventArgs e)
    {
        string nimi = await DisplayPromptAsync("Tere!", "Mis on sinu nimi?"); 
        if (!string.IsNullOrEmpty(nimi))
            await DisplayAlert("Tervitus", $"Tere, {nimi}!", "OK"); 
    }

    // Valiku tegemise meetod
    private async void ValikButton_Clicked(object sender, EventArgs e)
    {
        string valik = await DisplayActionSheet("Vali teema", "Loobu", null, "Loomad", "Toit", "Aju"); // Kuvab ActionSheet valikud
        await DisplayAlert("Valik", $"Valisid: {valik}", "OK"); 
    }
}
using System.Collections.ObjectModel;

namespace TARpe24_Mobiilirakendused;

public partial class Elemendid_Page : ContentPage
{
    public static ObservableCollection<Telefon> telefons { get; set; }

    ListView list;
    Entry entryNimetus, entryTootja, entryHind;

    string valitudPildiTee = "";
    Label lblValitudPilt;

    public class Telefon
    {
        public string Nimetus { get; set; }
        public string Tootja { get; set; }
        public int Hind { get; set; }
        public string Pilt { get; set; }
    }

    public Elemendid_Page()
    {
        telefons = new ObservableCollection<Telefon>
        {
            new Telefon { Nimetus = "Samsung Galaxy S22 Ultra", Tootja= "Samsung", Hind= 1349, Pilt= "Galaxy.png" },
            new Telefon { Nimetus = "Xiaomi Mi 11 Lite 5G NE", Tootja= "Xiaomi", Hind= 399, Pilt= "Xiaomi5GNE.png" },
            new Telefon { Nimetus = "iPhone 13 mini", Tootja = "Apple", Hind = 1179, Pilt = "iPhone13.png" }
        };

        // ENTRY väljad
        entryNimetus = new Entry { Placeholder = "Nimetus" };
        entryTootja = new Entry { Placeholder = "Tootja" };
        entryHind = new Entry { Placeholder = "Hind", Keyboard = Keyboard.Numeric };

        // Pildi valimise nupp
        Button btnValiPilt = new Button { Text = "📷 Vali pilt galeriist", BackgroundColor = Colors.LightBlue };
        btnValiPilt.Clicked += BtnValiPilt_Clicked;

        lblValitudPilt = new Label
        {
            Text = "Pilti pole valitud",
            FontSize = 12,
            TextColor = Colors.Gray
        };

        // Nupud
        Button btnLisa = new Button { Text = "Lisa" };
        btnLisa.Clicked += Lisa_Clicked;

        Button btnKustuta = new Button { Text = "Kustuta" };
        btnKustuta.Clicked += Kustuta_Clicked;

        // LISTVIEW
        list = new ListView
        {
            HasUnevenRows = true,
            ItemsSource = telefons,
            ItemTemplate = new DataTemplate(() =>
            {
                Image img = new Image { HeightRequest = 50, WidthRequest = 50, Aspect = Aspect.AspectFit };
                img.SetBinding(Image.SourceProperty, "Pilt");

                Label nimetus = new Label { FontSize = 18 };
                nimetus.SetBinding(Label.TextProperty, "Nimetus");

                Label hind = new Label();
                hind.SetBinding(Label.TextProperty, "Hind");

                StackLayout textLayout = new StackLayout
                {
                    Children = { nimetus, hind }
                };

                StackLayout row = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { img, textLayout }
                };

                return new ViewCell { View = row };
            })
        };

        list.ItemTapped += List_ItemTapped;

        // Kogu UI
        Content = new StackLayout
        {
            Padding = 10,
            Children =
            {
                entryNimetus,
                entryTootja,
                entryHind,
                btnValiPilt,
                lblValitudPilt,
                btnLisa,
                btnKustuta,
                list
            }
        };
    }

    // 3. PILDI VALIMINE
    private async void BtnValiPilt_Clicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                valitudPildiTee = photo.FullPath;
                lblValitudPilt.Text = $"Valitud: {photo.FileName}";
                lblValitudPilt.TextColor = Colors.Green;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", ex.Message, "OK");
        }
    }

    // 4. LISAMINE
    private void Lisa_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(entryNimetus.Text))
        {
            int hind = 0;
            int.TryParse(entryHind.Text, out hind);

            string pildiNimi = string.IsNullOrWhiteSpace(valitudPildiTee)
                ? "default_phone.png"
                : valitudPildiTee;

            telefons.Add(new Telefon
            {
                Nimetus = entryNimetus.Text,
                Tootja = entryTootja.Text,
                Hind = hind,
                Pilt = pildiNimi
            });

            entryNimetus.Text = "";
            entryTootja.Text = "";
            entryHind.Text = "";

            valitudPildiTee = "";
            lblValitudPilt.Text = "Pilti pole valitud";
            lblValitudPilt.TextColor = Colors.Gray;
        }
    }

    // 5. KUSTUTAMINE
    private async void Kustuta_Clicked(object sender, EventArgs e)
    {
        Telefon phone = list.SelectedItem as Telefon;

        if (phone != null)
        {
            bool confirm = await DisplayAlert("Kustutamine", "Kas oled kindel?", "Jah", "Ei");

            if (confirm)
            {
                telefons.Remove(phone);
                list.SelectedItem = null;
            }
        }
    }

    // 6. ITEM TAPPED
    private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Telefon selectedPhone = e.Item as Telefon;

        if (selectedPhone != null)
        {
            await DisplayAlert("Valitud mudel",
                $"{selectedPhone.Tootja} - {selectedPhone.Nimetus}",
                "OK");
        }
    }
}
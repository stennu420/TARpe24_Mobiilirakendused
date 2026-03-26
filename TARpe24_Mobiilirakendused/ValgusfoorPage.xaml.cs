using Microsoft.Maui.Controls.Shapes;

namespace TARpe24_Mobiilirakendused;

public partial class ValgusfoorPage : ContentPage
{
    Ellipse redBall;
    Ellipse yellowBall;
    Ellipse greenBall;

    VerticalStackLayout vsl;
    Label statusLabel;

    bool isOn = false;

    public ValgusfoorPage()
    {
        InitializeComponent();

        // Pealkiri
        statusLabel = new Label
        {
            Text = "Vali valgus",
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center
        };

        // Pallid
        redBall = CreateBall();
        yellowBall = CreateBall();
        greenBall = CreateBall();

        // Klikid pallidele
        AddTap(redBall, Colors.Red, "Seisa");
        AddTap(yellowBall, Colors.Yellow, "Valmista");
        AddTap(greenBall, Colors.Green, "Sõida");

        // Nupud
        Button sisseBtn = new Button { Text = "SISSE" };
        Button valjaBtn = new Button { Text = "VÄLJA" };

        sisseBtn.Clicked += OnSisseClicked;
        valjaBtn.Clicked += OnValjaClicked;

        HorizontalStackLayout buttons = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center,
            Children = { sisseBtn, valjaBtn }
        };

        // Layout
        vsl = new VerticalStackLayout
        {
            Padding = 30,
            Spacing = 20,
            Children =
            {
                statusLabel,
                redBall,
                yellowBall,
                greenBall,
                buttons
            }
        };

        Content = vsl;
    }

    // Ühe palli loomine
    Ellipse CreateBall()
    {
        return new Ellipse
        {
            WidthRequest = 120,
            HeightRequest = 120,
            Fill = new SolidColorBrush(Colors.Gray),
            Stroke = Colors.Black,
            StrokeThickness = 3,
            HorizontalOptions = LayoutOptions.Center
        };
    }

    // Kliki lisamine pallile
    void AddTap(Ellipse ball, Color color, string text)
    {
        var tap = new TapGestureRecognizer();

        tap.Tapped += (s, e) =>
        {
            if (!isOn)
            {
                statusLabel.Text = "Lülita foor sisse";
                return;
            }

            SetOff();
            ball.Fill = new SolidColorBrush(color);
            statusLabel.Text = text;
        };

        ball.GestureRecognizers.Add(tap);
    }

    // SISSE
    void OnSisseClicked(object sender, EventArgs e)
    {
        isOn = true;

        redBall.Fill = new SolidColorBrush(Colors.Red);
        yellowBall.Fill = new SolidColorBrush(Colors.Yellow);
        greenBall.Fill = new SolidColorBrush(Colors.Green);

        statusLabel.Text = "Valgusfoor sees";
    }

    // VÄLJA
    void OnValjaClicked(object sender, EventArgs e)
    {
        isOn = false;
        SetOff();
        statusLabel.Text = "Valgusfoor väljas";
    }

    // Kõik halliks
    void SetOff()
    {
        redBall.Fill = new SolidColorBrush(Colors.Gray);
        yellowBall.Fill = new SolidColorBrush(Colors.Gray);
        greenBall.Fill = new SolidColorBrush(Colors.Gray);
    }
}
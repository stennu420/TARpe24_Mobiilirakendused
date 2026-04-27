using System.Collections.ObjectModel;

namespace TARpe24_Mobiilirakendused;

public partial class InteraktiivneGaleri : ContentPage
{
	
	public class CarouselItem
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string DescriptionEt { get; set; }
        public string DescriptionEn { get; set; }
        public string DetailEt { get; set; }
        public string DetailEn { get; set; }
    }

    private CarouselView carouselView;
    private ObservableCollection<CarouselItem> items;
    private int position = 0;
    private bool isEstonian = true;

    public InteraktiivneGaleri()
    {
        items = new ObservableCollection<CarouselItem>
        {
            new CarouselItem
            {
                Title = "Tarkvaraarendaja",
                ImageUrl = "https://picsum.photos/id/180/600/400",
                DescriptionEt = "Loob rakendusi ja süsteeme",
                DescriptionEn = "Builds applications and systems",
                DetailEt = "Vajalikud oskused:\nC#, Java, Python, Git, andmebaasid, probleemilahendus",
                DetailEn = "Required skills:\nC#, Java, Python, Git, databases, problem solving"
            },
            new CarouselItem
            {
                Title = "Kvaliteedispetsialist (QA)",
                ImageUrl = "https://picsum.photos/id/201/600/400",
                DescriptionEt = "Tagab tarkvara töökindluse",
                DescriptionEn = "Ensures software reliability",
                DetailEt = "Vajalikud oskused:\nTestimine, testplaanid, Selenium, Postman, veatuvastus",
                DetailEn = "Required skills:\nTesting, test plans, Selenium, Postman, bug tracking"
            },
            new CarouselItem
            {
                Title = "UI/UX Disainer",
                ImageUrl = "https://picsum.photos/id/20/600/400",
                DescriptionEt = "Kujundab kasutajasõbralikke liideseid",
                DescriptionEn = "Creates user-friendly interfaces",
                DetailEt = "Vajalikud oskused:\nFigma, prototüüpimine, kasutajakogemus, värviteooria, disainisüsteemid",
                DetailEn = "Required skills:\nFigma, prototyping, user experience, color theory, design systems"
            },
            new CarouselItem
            {
                Title = "Süsteemiadministraator",
                ImageUrl = "https://picsum.photos/id/60/600/400",
                DescriptionEt = "Haldab IT-infrastruktuuri",
                DescriptionEn = "Manages IT infrastructure",
                DetailEt = "Vajalikud oskused:\nLinux, Windows Server, võrgud, pilveteenused, turvalisus",
                DetailEn = "Required skills:\nLinux, Windows Server, networking, cloud services, security"
            },
            new CarouselItem
            {
                Title = "Andmeanalüütik",
                ImageUrl = "https://picsum.photos/id/48/600/400",
                DescriptionEt = "Muudab andmed kasulikuks infoks",
                DescriptionEn = "Turns data into insights",
                DetailEt = "Vajalikud oskused:\nSQL, Excel, Power BI, Python, statistika, andmete visualiseerimine",
                DetailEn = "Required skills:\nSQL, Excel, Power BI, Python, statistics, data visualization"
            }
        };

        BuildPage();

        Device.StartTimer(TimeSpan.FromSeconds(4), () =>
        {
            if (items.Count == 0)
                return false;

            position = (position + 1) % items.Count;
            carouselView.Position = position;

            return true;
        });
    }

    private void BuildPage()
    {
        Title = isEstonian ? "IT-karjääri kompass" : "IT Career Compass";

        var titleLabel = new Label
        {
            Text = isEstonian ? "IT-karjääri kompass " : "IT Career Compass ",
            FontSize = 28,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            HorizontalTextAlignment = TextAlignment.Center
        };

        var languagePicker = new Picker
        {
            Title = isEstonian ? "Vali keel" : "Choose language",
            BackgroundColor = Colors.White,
            TextColor = Colors.Black
        };

        languagePicker.Items.Add("Eesti");
        languagePicker.Items.Add("English");
        languagePicker.SelectedIndex = isEstonian ? 0 : 1;

        languagePicker.SelectedIndexChanged += (sender, e) =>
        {
            isEstonian = languagePicker.SelectedIndex == 0;
            BuildPage();
        };

        carouselView = new CarouselView
        {
            ItemsSource = items,
            HeightRequest = 430,
            PeekAreaInsets = new Thickness(35, 0, 35, 0),

            ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame
                {
                    CornerRadius = 25,
                    HasShadow = true,
                    Padding = 0,
                    Margin = new Thickness(8),
                    BackgroundColor = Colors.White
                };

                var grid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(270) },
                        new RowDefinition { Height = GridLength.Auto }
                    }
                };

                var image = new Image
                {
                    Aspect = Aspect.AspectFill
                };

                image.SetBinding(Image.SourceProperty, "ImageUrl");

                var textLayout = new VerticalStackLayout
                {
                    Padding = 18,
                    Spacing = 8
                };

                var nameLabel = new Label
                {
                    FontSize = 24,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.DarkSlateBlue,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                nameLabel.SetBinding(Label.TextProperty, "Title");

                var descriptionLabel = new Label
                {
                    FontSize = 15,
                    TextColor = Colors.DimGray,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                descriptionLabel.SetBinding(
                    Label.TextProperty,
                    new Binding(isEstonian ? "DescriptionEt" : "DescriptionEn")
                );

                textLayout.Children.Add(nameLabel);
                textLayout.Children.Add(descriptionLabel);

                grid.Children.Add(image);
                Grid.SetRow(image, 0);

                grid.Children.Add(textLayout);
                Grid.SetRow(textLayout, 1);

                frame.Content = grid;

                var tapGesture = new TapGestureRecognizer();

                tapGesture.Tapped += async (s, e) =>
                {
                    if (frame.BindingContext is CarouselItem item)
                    {
                        await frame.ScaleTo(0.95, 100);
                        await frame.ScaleTo(1, 100);

                        await DisplayAlert(
                            item.Title,
                            isEstonian ? item.DetailEt : item.DetailEn,
                            "OK"
                        );
                    }
                };

                frame.GestureRecognizers.Add(tapGesture);

                return frame;
            })
        };

        var indicatorView = new IndicatorView
        {
            IndicatorColor = Colors.LightGray,
            SelectedIndicatorColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 10)
        };

        carouselView.IndicatorView = indicatorView;

        Content = new ScrollView
        {
            Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops =
                {
                    new GradientStop(Colors.DarkSlateBlue, 0),
                    new GradientStop(Colors.MediumPurple, 1)
                }
            },

            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 20,
                Children =
                {
                    titleLabel,
                    languagePicker,
                    carouselView,
                    indicatorView
                }
            }
        };
    }
}

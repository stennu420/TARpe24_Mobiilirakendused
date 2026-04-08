namespace TARpe24_Mobiilirakendused;

public partial class KontaktPage : ContentPage
{
    //UI elemendid 
    Entry email_phone; //e-maili või telefoni sisend
    Entry message;
    Label label;
    Label s6numLabel; // sõnumi juhiste jaoks
    List<string> To;
    Button buttonSms;
    Button buttonEmail;
    public KontaktPage() //Elemendi ja paigutus
    {
        //juhised
        label = new Label
        {
            Text = "Sisesta oma kontaktandmed"
        };
        email_phone = new Entry
        {
            Placeholder = "E-mail või Telefoni number",
            Keyboard = Keyboard.Default
        };
        s6numLabel = new Label
        {
            Text = "Sisesta oma sõnum siia"
        };
        message = new Entry
        {
            Placeholder = "Tere maailm!",
            Keyboard = Keyboard.Default
        };

        Button buttonSms = new Button
        {
            Text = "Saada SMS",
            FontSize = 18,
            BackgroundColor = Colors.DodgerBlue,
            TextColor = Colors.White
        };
        buttonSms.Clicked += Saada_sms_Clicked; // ühendab klikisündmuse SMS saatmise meetodiga

        Button buttonEmail = new Button
        {
            Text = "Saada Meil",
            FontSize = 18,
            BackgroundColor = Colors.MediumSlateBlue,
            TextColor = Colors.White
        };
        buttonEmail.Clicked += Saada_email_Clicked;  // ühendab klikisündmuse e-maili saatmise meetodiga

        Content = new VerticalStackLayout
        {
            Spacing = 22,
            Children =
            {
                new Label
                {
                    Text = "Kontaktid",
                    FontSize = 28,
                    HorizontalOptions = LayoutOptions.Center
                },
                label,
                email_phone,
                s6numLabel,
                message,
                buttonSms,
                buttonEmail
            }
        };
    }
    // SMS saatmine
    private async void Saada_sms_Clicked(object? sender, EventArgs e)
    {
        string phone = email_phone.Text;
        var message = "Tere tulemast! Saadan sõnumi";
        SmsMessage sms = new SmsMessage(message, phone); // sms objekt
        if (phone != null && Sms.Default.IsComposeSupported) 
        {
            await Sms.Default.ComposeAsync(sms); // koostamine
        }
    }
    // E-maili saatmine
    private async void Saada_email_Clicked(object? sender, EventArgs e)
    {
        var message = "Tere tulemast! Saada email";
        EmailMessage e_mail = new EmailMessage
        {
            Subject = email_phone.Text,
            Body = message,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string>(new[] { email_phone.Text }) //saaja aadress
        };
        if (Email.Default.IsComposeSupported) // saatmine
        {
            await Email.Default.ComposeAsync(e_mail);
        }
        else
        {
            await DisplayAlertAsync("Viga", "E-maili saatmine pole selles seadmes toetatud", "OK");
        }
    }
}
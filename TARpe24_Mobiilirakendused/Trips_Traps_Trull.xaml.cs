namespace TARpe24_Mobiilirakendused;

public partial class Trips_Traps_Trull : ContentPage
{
    Button[,] buttons = new Button[3, 3]; // 3x3 mänguväli
    bool xTurn = true; // kelle kord (true = X, false = O)

    public Trips_Traps_Trull()
    {
        InitializeComponent();

        Grid grid = new Grid();

        // Loome 3x3 gridi
        for (int i = 0; i < 3; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        // Loome nupud igasse ruutu
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                Button btn = new Button
                {
                    FontSize = 40,
                    BackgroundColor = Colors.LightGray
                };

                btn.Clicked += OnCellClicked;

                buttons[r, c] = btn;
                grid.Add(btn, c, r);
            }
        }

        // Uus mäng nupp
        Button resetBtn = new Button { Text = "Alusta mäng" };
        resetBtn.Clicked += ResetGame;

        // Kes alustab nupp
        Button firstBtn = new Button { Text = "Kes on esimene?" };
        firstBtn.Clicked += FirstPlayer;

        Content = new VerticalStackLayout
        {
            Padding = 20,
            Children = { grid, resetBtn, firstBtn }
        };
    }

    // Kui vajutad ruudule
    private async void OnCellClicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (btn.Text != "") return; // kui juba täidetud

        btn.Text = xTurn ? "X" : "O";

        if (CheckWinner())
        {
            await DisplayAlert("Võit!", $"{btn.Text} võitis!", "OK");
            ResetGame(null, null);
            return;
        }

        if (IsDraw())
        {
            await DisplayAlert("Viik", "Mäng lõppes viigiga", "OK");
            ResetGame(null, null);
            return;
        }

        xTurn = !xTurn; // vaheta mängijat
    }

    // Kontrollib võitu
    private bool CheckWinner()
    {
        string[,] board = new string[3, 3];

        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                board[r, c] = buttons[r, c].Text;

        // read ja veerud
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return true;

            if (board[0, i] != "" && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                return true;
        }

        // diagonaalid
        if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            return true;

        if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            return true;

        return false;
    }

    // Kontrollib viiki
    private bool IsDraw()
    {
        foreach (var btn in buttons)
        {
            if (btn.Text == "")
                return false;
        }
        return true;
    }

    // Uus mäng
    private void ResetGame(object sender, EventArgs e)
    {
        foreach (var btn in buttons)
        {
            btn.Text = "";
        }
        xTurn = true;
    }

    // Juhuslik esimene mängija
    private async void FirstPlayer(object sender, EventArgs e)
    {
        Random rnd = new Random();
        xTurn = rnd.Next(2) == 0;

        await DisplayAlert("Algus", xTurn ? "X alustab" : "O alustab", "OK");
    }
}
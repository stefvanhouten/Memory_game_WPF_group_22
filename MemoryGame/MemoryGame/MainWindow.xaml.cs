using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryGame
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CheckBox lastChecked;

        /*  We use delegates to update Form1 from another Thread. 
         *  C# won't allow the UI thread to be update from another thread, for instance from our Memory game class. 
         *  
         *  To bypass this we create a delegate. When we call one of our delegates we do an invokeRequired check,
         *  all this does is it validates if the method was called from another thread. If so we invoke our delegate 
         *  which then handles whatever it is we want to do. 
         */
        public delegate void SetPlayerScoreCallback(string playerOneLabel, string playerTwoLabel);
        public delegate void SetCurrentPlayerCallback(string currentPlayer);
        public delegate void RedirectToHighScoresCallback();
        public delegate void ClearPanelsCallback();
        public delegate void ShowLoadSaveGameCallback();
        public delegate void UpdateHighScoresTableCallback();

        readonly Memory game;

        public MainWindow()
        {
            InitializeComponent();
            this.game = new Memory(MemoryGrid, this);
        }

        public void ShowLoadGameCheckbox()
        {

        }

        public void ClearPanels()
        {
            this.MemoryGrid.RowDefinitions.Clear();
            this.MemoryGrid.ColumnDefinitions.Clear();
        }

        public void UpdateHighScoresTable()
        {

        }

        public void UpdateCurrentPlayer(string currentPlayer)
        {

        }

        public void UpdateScore(string playerOneLabel, string playerTwoLabel)
        {
            
        }

        /// <summary>
        /// Populate the dropdown menu with available gamesizes. 
        /// </summary>
        private void GenerateGameSizeDropDownOptions()
        {
            this.GameSizeComboBox.Items.Clear();
            int totalCards = this.game.TotalCardsInCurrentTheme();
            foreach (GameOptions item in this.game.GameOptions)
            {
                if (totalCards >= item.CardsRequired)
                {
                    this.GameSizeComboBox.Items.Add(item);
                }
            }
            this.GameSizeComboBox.SelectedIndex = 0;
        }


        /// <summary>
        /// Validates that two player names have been provided. 
        /// If so create two new instances of Player and add them to the Memory.Players array.
        /// Personalize the memory game playing field by changing the labels to the player names.
        /// Call for the memory class to start the game, this will create a playing field.
        /// After this is done we switch to the playing field.
        /// Raises: 
        ///     MessageBox: MessageBox.Show is called whenever user fails to provide two player names
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartMemoryGame(object sender, EventArgs e)
        {
            string playerOne = this.InputPlayer1.Text;
            string playerTwo = this.InputPlayer2.Text;
            if (String.IsNullOrEmpty(playerOne) || String.IsNullOrEmpty(playerTwo))
            {
                MessageBox.Show("Player names cannot be empty", "Validation error");
                return;
            }

            if (playerOne.Equals(playerTwo))
            {
                MessageBox.Show("Player names cannot be the same!", "Input error");
                return;
            }

            //Should probably make a method for adding new players to the game. This can be exploited
            this.game.Players[0] = new Player(playerOne);
            this.game.Players[1] = new Player(playerTwo);
            LabelPlayerOneScore.Content = $"{this.game.Players[0].Name} : {this.game.Players[0].ScoreBoard.Score}";
            LabelPlayerTwoScore.Content = $"{this.game.Players[1].Name} : {this.game.Players[1].ScoreBoard.Score}";
            LabelCurrentPlayer.Content = $"Current player: {this.game.Players[0].Name}";
            this.game.StartGame();
            this.GeneratePlayingField();
            TabMemoryGame.IsSelected = true;
        }

     

        private void GeneratePlayingField()
        {
            for (int i = 0; i < this.game.Rows; i++)
            {
                this.MemoryGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < this.game.Collumns; i++)
            {
                this.MemoryGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            int index = 0;
            for (int i = 0; i < this.game.Rows; i++)
            {
                for (int x = 0; x < this.game.Collumns; x++)
                {
                    Card cardBtn = this.game.Deck[index];
                    var border = new Border();
                    var image = new System.Windows.Controls.Image();

                    border.Child = image;
                    image.Stretch = Stretch.Fill;
                    cardBtn.Content = border;
                    cardBtn.Name = $"b{index}";
                    cardBtn.Click += new RoutedEventHandler(this.game.CardClicked);

                    this.MemoryGrid.Children.Add(cardBtn);
                    Grid.SetRow(cardBtn, i);
                    Grid.SetColumn(cardBtn, x);
                    index++;
                }
            }
        }

        //public void CardClicked(object sender, EventArgs e)
        //{
        //    Button test = (Button)sender;
        //    Card clickedCard = this.game.Deck[Convert.ToInt32(test.Name.Substring(1))];

        //    if (this.game.GameIsFrozen || this.game.SelectedCards.Contains(clickedCard) || clickedCard.IsSolved)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        var imageSource = this.BitmapToImageSource(clickedCard.CardImage);
        //        var border = new Border();
        //        var image = new System.Windows.Controls.Image();
        //        border.Child = image;
        //        image.Source = imageSource;
        //        image.Stretch = Stretch.Fill;
        //        test.Content = border;
        //        this.game.SelectedCards.Add(clickedCard);
        //    }
        //}
        /// <summary>
        /// Handles behaviour for themeselection. Forces one theme to be selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameSizeSelection(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            GameOptions options = (GameOptions)comboBox.SelectedItem;
            this.game.Rows = options.Rows;
            this.game.Collumns = options.Columns;
        }

        public void NavigateHome(object sender, EventArgs e)
        {
            TabHome.IsSelected = true;
        }

        public void NavigatePreGame(object sender, EventArgs e)
        {
            this.GenerateGameSizeDropDownOptions();
            TabPreGame.IsSelected = true;
        }

        public void NavigateToHighScores()
        {
            TabHighScores.IsSelected = true;
        }

        public void NavigateHighScores(object sender, EventArgs e)
        {
            TabHighScores.IsSelected = true;
        }

        public void NavigateThemeSelection(object sender, EventArgs e)
        {
            TabThemeSelection.IsSelected = true;
        }
    }

}

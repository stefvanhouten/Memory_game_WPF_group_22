using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MemoryGame
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CheckBox lastChecked;
        readonly Memory game;

        public MainWindow()
        {
            InitializeComponent();
            this.game = new Memory(this);
            this.RenderBackgroundImage();
            this.GenerateThemeSelectionCheckboxes();
        }

        /// <summary>
        /// Dynamicly creates checkboxes for each available theme in the memory game.
        /// </summary>
        private void GenerateThemeSelectionCheckboxes()
        {
            int totalChildren = this.ThemeGrid.Children.Count;
            for (int x = 0; x < totalChildren - 1; x++)
            {
                if(this.ThemeGrid.Children[x].GetType() == typeof(CheckBox))
                {
                    this.ThemeGrid.Children.Remove(this.ThemeGrid.Children[x]);
                }
            }
            int i = 0;
            foreach (KeyValuePair<int, string> theme in this.game.Theme)
            {
                CheckBox checkBox = new CheckBox()
                {
                    Name = $"b{theme.Key}",
                    Content = theme.Value,
                    Margin = new Thickness(5, 40 + i * 20, 0, 0)
                };
                if (i == this.game.SelectedTheme)
                {
                    checkBox.IsChecked = true;
                    this.lastChecked = checkBox;
                }
                checkBox.Click += CheckBox_Click;
                ThemeGrid.Children.Add(checkBox);
                Grid.SetRow(checkBox, 0);
                i++;
            }
        }

        /// <summary>
        /// Handles behaviour for themeselection. Forces one theme to be selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, EventArgs e)
        {
            CheckBox activeCheckBox = sender as CheckBox;
            //Handle toggling behaviour in the checkboxes. Don't allow unselecting only active checkbox
            if (activeCheckBox != lastChecked && lastChecked != null)
                this.lastChecked.IsChecked = false;
            else
                this.lastChecked.IsChecked = true;

            lastChecked = (bool)activeCheckBox.IsChecked ? activeCheckBox : null;
            //Set the currently selected theme
            this.game.SelectedTheme = Convert.ToInt32(activeCheckBox.Name.Substring(1));
            this.RenderBackgroundImage();
           
            //Re-generate the options that are displayed in the pre game view based on selected theme. 
        }

        /// <summary>
        /// Updates the application background based on selected theme.
        /// </summary>
        private void RenderBackgroundImage()
        {
            ImageBrush background = new ImageBrush()
            {
                ImageSource = Memory.BitmapToImageSource(this.game.BackgroundTheme[this.game.SelectedTheme]),
                Opacity = 0.5
            };
            this.HomeGrid.Background = this.ThemeGrid.Background = this.MemoryGrid.Background = 
            this.PreGameGrid.Background = this.HighScoresDataGrid.Background = background;
        }

        /// <summary>
        /// Displays the loadgame checkbox when a savagame file is available.
        /// </summary>
        public void ShowLoadGameCheckbox()
        {
            if (this.game.HasUnfinishedGame)
            {
                this.LoadSaveGameCheckBox.Visibility = Visibility.Visible;
            }
            else
            {
                this.LoadSaveGameCheckBox.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Clears the memory grid rows and collumns
        /// </summary>
        public void ClearPanels()
        {
            this.MemoryGrid.RowDefinitions.Clear();
            this.MemoryGrid.ColumnDefinitions.Clear();
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
            this.game.AddPlayers(playerOne, playerTwo);
            LabelPlayerOneScore.Content = $"{this.game.Players[0].Name} : {this.game.Players[0].ScoreBoard.Score}";
            LabelPlayerTwoScore.Content = $"{this.game.Players[1].Name} : {this.game.Players[1].ScoreBoard.Score}";
            LabelCurrentPlayer.Content = $"Current player: {this.game.Players[0].Name}";
            this.game.StartGame();
            this.GeneratePlayingField();
            TabMemoryGame.IsSelected = true;
        }

        /// <summary>
        /// Mute any audio playback if this is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MuteAudio(object sender, EventArgs e)
        {
            this.game.AudioIsEnabled = false;
        }

        /// <summary>
        /// When the loadgame checkbox is checked starts the process of retrieving gamestate from savefile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadSavedGame(object sender, EventArgs e)
        {
            this.game.ResumeGame(loadFromSaveFile: true);
            this.RenderBackgroundImage();
            this.GenerateThemeSelectionCheckboxes();
            TabMemoryGame.IsSelected = true;
        }

        /// <summary>
        /// Generates the playing field for the memory game based on game settings.
        /// </summary>
        public void GeneratePlayingField()
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
                    var image = new System.Windows.Controls.Image() { 
                        Stretch = Stretch.Fill, 
                        Source = Memory.BitmapToImageSource(this.game.CardFrontSide[this.game.SelectedTheme]) 
                    };

                    if (cardBtn.IsSolved || this.game.SelectedCards.Contains(cardBtn))
                    {
                        int position = this.game.ThemeImages[this.game.SelectedTheme].FindIndex(c => c.Name == cardBtn.PairName);
                        var imageSource = Memory.BitmapToImageSource(this.game.ThemeImages[this.game.SelectedTheme][position].Resource);
                        image.Source = imageSource;
                    }

                    cardBtn.Content = image;
                    cardBtn.Name = $"b{index}";
                    cardBtn.Click += new RoutedEventHandler(this.game.CardClicked);

                    this.MemoryGrid.Children.Add(cardBtn);
                    Grid.SetRow(cardBtn, i);
                    Grid.SetColumn(cardBtn, x);
                    index++;
                }
            }
        }

        /// <summary>
        /// Clean up the application after a game of memory is completed.
        /// </summary>
        public void CleanupAfterGame()
        {
            this.NavigateToHighScores();
            this.InputPlayer1.Text = "";
            this.InputPlayer2.Text = "";
            this.ClearPanels();
        }

        /// <summary>
        /// After each turn update the scoreboard and the current playing player label.
        /// </summary>
        /// <param name="playerOne"></param>
        /// <param name="playerTwo"></param>
        public void UpdateScoreBoardAndCurrentPlayer(Player playerOne, Player playerTwo)
        {
            if(playerOne != null && playerTwo != null)
            {
                this.LabelPlayerOneScore.Content = $"{playerOne.Name} : {playerOne.ScoreBoard.Score}";
                this.LabelPlayerTwoScore.Content = $"{playerTwo.Name} : {playerTwo.ScoreBoard.Score}";
                this.LabelCurrentPlayer.Content = !this.game.IsPlayerOnesTurn ? $"Current player: {playerOne.Name}" : $"Current player: {playerTwo.Name}";
            }

        }

        /// <summary>
        /// Reset the memory game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ResetMemory(object sender, RoutedEventArgs e)
        {
            this.game.ResetGame();
        }

        /// <summary>
        /// Pause or resume memory based on game state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PauseResumeMemory(object sender, RoutedEventArgs e)
        {
            if(this.PauseResumeBtn.Content.ToString().Contains("Pause"))
            {
                this.game.PauseGame();
                this.PauseResumeBtn.Content = "Resume";
            }
            else
            {
                this.game.ResumeGame();
                this.PauseResumeBtn.Content = "Pause";
            }
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
            GameOptions options = (GameOptions)this.GameSizeComboBox.SelectedItem;
            this.game.Rows = options.Rows;
            this.game.Collumns = options.Columns;
        }

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

        /// <summary>
        /// Navigate to the home screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NavigateHome(object sender, EventArgs e)
        {
            TabHome.IsSelected = true;
        }

        /// <summary>
        /// Navigate to thje pre game screen, also prepares available selections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NavigatePreGame(object sender, EventArgs e)
        {
            this.ShowLoadGameCheckbox();
            this.GenerateGameSizeDropDownOptions();
            TabPreGame.IsSelected = true;
        }

        /// <summary>
        /// Build the highscores table and navigate to the highscores.
        /// </summary>
        public void NavigateToHighScores()
        {
            ObservableCollection<HighScoreListing> oc = new ObservableCollection<HighScoreListing>();
            foreach (HighScoreListing item in this.game.HighScores.Limit(15))
            {
                oc.Add(item);
            }
            this.HighScoresDataGrid.ItemsSource = oc;
            TabHighScores.IsSelected = true;
        }

        /// <summary>
        /// Navigate to highscores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NavigateHighScores(object sender, EventArgs e)
        {
            this.NavigateToHighScores();
        }

        /// <summary>
        /// Navigate to theme selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NavigateThemeSelection(object sender, EventArgs e)
        {
            TabThemeSelection.IsSelected = true;
        }
    }

}

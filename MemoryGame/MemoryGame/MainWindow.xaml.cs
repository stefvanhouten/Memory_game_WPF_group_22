using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        readonly Memory game;

        public MainWindow()
        {
            InitializeComponent();
            this.game = new Memory(MemoryGrid, this);
            this.GenerateThemeSelectionCheckboxes();
        }

        private void GenerateThemeSelectionCheckboxes()
        {
            int i = 0;
            foreach (KeyValuePair<int, string> theme in this.game.Theme)
            {
                CheckBox checkBox = new CheckBox();
                if (i == 0)
                {
                    checkBox.IsChecked = true;
                    this.lastChecked = checkBox;
                }
                checkBox.Name = $"b{theme.Key}";
                checkBox.Content = theme.Value;
                checkBox.Margin = new Thickness(5, 40 + i * 20, 0, 0);
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
            //Re-generate the options that are displayed in the pre game view based on selected theme. 
        }

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

        public void ClearPanels()
        {
            this.MemoryGrid.RowDefinitions.Clear();
            this.MemoryGrid.ColumnDefinitions.Clear();
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
            this.game.AddPlayers(playerOne, playerTwo);
            LabelPlayerOneScore.Content = $"{this.game.Players[0].Name} : {this.game.Players[0].ScoreBoard.Score}";
            LabelPlayerTwoScore.Content = $"{this.game.Players[1].Name} : {this.game.Players[1].ScoreBoard.Score}";
            LabelCurrentPlayer.Content = $"Current player: {this.game.Players[0].Name}";
            this.game.StartGame();
            this.GeneratePlayingField();
            TabMemoryGame.IsSelected = true;
        }

        public void LoadSavedGame(object sender, EventArgs e)
        {
            this.game.ResumeGame(loadFromSaveFile: true);
            TabMemoryGame.IsSelected = true;
        }

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
                    var image = new System.Windows.Controls.Image();
                    if (cardBtn.IsSolved || this.game.SelectedCards.Contains(cardBtn))
                    {
                        int position = this.game.ThemeImages[this.game.SelectedTheme].FindIndex(c => c.Name == cardBtn.PairName);
                        var imageSource = this.game.BitmapToImageSource(this.game.ThemeImages[this.game.SelectedTheme][position].Resource);
                        image.Source = imageSource;
                    }

                    image.Stretch = Stretch.Fill;
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
            this.ShowLoadGameCheckbox();
            this.GenerateGameSizeDropDownOptions();
            TabPreGame.IsSelected = true;
        }

        public void NavigateToHighScores()
        {
            this.HighScoresDataGrid.ItemsSource = this.game.HighScores.highScores;
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

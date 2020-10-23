using MemoryGame.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryGame
{
    /// <summary>
    /// Base class for the Memory game.
    /// </summary>
    public class Memory
    {
        private readonly string SaveGamePath = Path.Combine(Directory.GetCurrentDirectory(), "savegame.txt");

        #region Large dictionary containing card images and card names
        public Dictionary<int, List<CardNameAndImage>> ThemeImages { get; private set; } = new Dictionary<int, List<CardNameAndImage>>()
        {
            { 0, new List<CardNameAndImage>()
                {
                    new CardNameAndImage() { Name = "Aussie", Resource = Resources.aussie },
                    new CardNameAndImage() { Name = "Aussie", Resource = Resources.aussie },
                    new CardNameAndImage() { Name = "black wolf", Resource = Resources.black_wolf },
                    new CardNameAndImage() { Name = "black wolf", Resource = Resources.black_wolf },
                    new CardNameAndImage() { Name = "cavia", Resource = Resources.cavia },
                    new CardNameAndImage() { Name = "cavia", Resource = Resources.cavia },
                    new CardNameAndImage() { Name = "cheetah", Resource = Resources.cheetah },
                    new CardNameAndImage() { Name = "cheetah", Resource = Resources.cheetah },
                    new CardNameAndImage() { Name = "deer", Resource = Resources.deer },
                    new CardNameAndImage() { Name = "deer", Resource = Resources.deer },
                    new CardNameAndImage() { Name = "egel", Resource = Resources.egel },
                    new CardNameAndImage() { Name = "egel", Resource = Resources.egel },
                    new CardNameAndImage() { Name = "elephant", Resource = Resources.elephant },
                    new CardNameAndImage() { Name = "elephant", Resource = Resources.elephant },
                    new CardNameAndImage() { Name = "giraffe", Resource = Resources.giraffe },
                    new CardNameAndImage() { Name = "giraffe", Resource = Resources.giraffe },
                    new CardNameAndImage() { Name = "gorilla", Resource = Resources.gorilla },
                    new CardNameAndImage() { Name = "gorilla", Resource = Resources.gorilla },
                    new CardNameAndImage() { Name = "horse", Resource = Resources.horse },
                    new CardNameAndImage() { Name = "horse", Resource = Resources.horse },
                    new CardNameAndImage() { Name = "monkey", Resource = Resources.monkey },
                    new CardNameAndImage() { Name = "monkey", Resource = Resources.monkey },
                    new CardNameAndImage() { Name = "oerangutan", Resource = Resources.oerangutan },
                    new CardNameAndImage() { Name = "oerangutan", Resource = Resources.oerangutan },
                    new CardNameAndImage() { Name = "orca", Resource = Resources.orca },
                    new CardNameAndImage() { Name = "orca", Resource = Resources.orca },
                    new CardNameAndImage() { Name = "parrot", Resource = Resources.parrot },
                    new CardNameAndImage() { Name = "parrot", Resource = Resources.parrot },
                    new CardNameAndImage() { Name = "penguin", Resource = Resources.penguin },
                    new CardNameAndImage() { Name = "penguin", Resource = Resources.penguin },
                    new CardNameAndImage() { Name = "polar bear", Resource = Resources.polar_bear },
                    new CardNameAndImage() { Name = "polar bear", Resource = Resources.polar_bear },
                    new CardNameAndImage() { Name = "rabbit", Resource = Resources.rabbit },
                    new CardNameAndImage() { Name = "rabbit", Resource = Resources.rabbit },
                    new CardNameAndImage() { Name = "red canary", Resource = Resources.red_canary },
                    new CardNameAndImage() { Name = "red canary", Resource = Resources.red_canary },
                    new CardNameAndImage() { Name = "red panda", Resource = Resources.red_panda },
                    new CardNameAndImage() { Name = "red panda", Resource = Resources.red_panda },
                    new CardNameAndImage() { Name = "rhino", Resource = Resources.rhino },
                    new CardNameAndImage() { Name = "rhino", Resource = Resources.rhino },
                    new CardNameAndImage() { Name = "seal", Resource = Resources.seal },
                    new CardNameAndImage() { Name = "seal", Resource = Resources.seal },
                    new CardNameAndImage() { Name = "shark", Resource = Resources.shark },
                    new CardNameAndImage() { Name = "shark", Resource = Resources.shark },
                    new CardNameAndImage() { Name = "sloth", Resource = Resources.sloth },
                    new CardNameAndImage() { Name = "sloth", Resource = Resources.sloth },
                    new CardNameAndImage() { Name = "tiger", Resource = Resources.tiger },
                    new CardNameAndImage() { Name = "tiger", Resource = Resources.tiger },
                    new CardNameAndImage() { Name = "turtle", Resource = Resources.turtle },
                    new CardNameAndImage() { Name = "turtle", Resource = Resources.turtle },
                    new CardNameAndImage() { Name = "ugly fish", Resource = Resources.ugly_fish },
                    new CardNameAndImage() { Name = "ugly fish", Resource = Resources.ugly_fish },
                    new CardNameAndImage() { Name = "jan_van_gent", Resource = Resources.jan_van_gent },
                    new CardNameAndImage() { Name = "jan_van_gent", Resource = Resources.jan_van_gent },
                    new CardNameAndImage() { Name = "hamster", Resource = Resources.hamster },
                    new CardNameAndImage() { Name = "hamster", Resource = Resources.hamster },
                    new CardNameAndImage() { Name = "fangtooth", Resource = Resources.fangtooth },
                    new CardNameAndImage() { Name = "fangtooth", Resource = Resources.fangtooth },
                    new CardNameAndImage() { Name = "piranha", Resource = Resources.piranha },
                    new CardNameAndImage() { Name = "piranha", Resource = Resources.piranha },
                }
            },
            { 1, new List<CardNameAndImage>()
                {
                    new CardNameAndImage() { Name = "aragorn", Resource = Resources.aragorn },
                    new CardNameAndImage() { Name = "aragorn", Resource = Resources.aragorn },
                    new CardNameAndImage() { Name = "argonath", Resource = Resources.argonath },
                    new CardNameAndImage() { Name = "argonath", Resource = Resources.argonath },
                    new CardNameAndImage() { Name = "boromir", Resource = Resources.boromir },
                    new CardNameAndImage() { Name = "boromir", Resource = Resources.boromir },
                    new CardNameAndImage() { Name = "elrond", Resource = Resources.elrond },
                    new CardNameAndImage() { Name = "elrond", Resource = Resources.elrond },
                    new CardNameAndImage() { Name = "eomer", Resource = Resources.eomer },
                    new CardNameAndImage() { Name = "eomer", Resource = Resources.eomer },
                    new CardNameAndImage() { Name = "Eowyn", Resource = Resources.Eowyn },
                    new CardNameAndImage() { Name = "Eowyn", Resource = Resources.Eowyn },
                    new CardNameAndImage() { Name = "faramir", Resource = Resources.faramir },
                    new CardNameAndImage() { Name = "faramir", Resource = Resources.faramir },
                    new CardNameAndImage() { Name = "frodo", Resource = Resources.frodo },
                    new CardNameAndImage() { Name = "frodo", Resource = Resources.frodo },
                    new CardNameAndImage() { Name = "Galadriel", Resource = Resources.Galadriel },
                    new CardNameAndImage() { Name = "Galadriel", Resource = Resources.Galadriel },
                    new CardNameAndImage() { Name = "Gandalf", Resource = Resources.Gandalf },
                    new CardNameAndImage() { Name = "Gandalf", Resource = Resources.Gandalf },
                    new CardNameAndImage() { Name = "gollum", Resource = Resources.gollum },
                    new CardNameAndImage() { Name = "gollum", Resource = Resources.gollum },
                    new CardNameAndImage() { Name = "gothmog", Resource = Resources.gothmog },
                    new CardNameAndImage() { Name = "gothmog", Resource = Resources.gothmog },
                    new CardNameAndImage() { Name = "high elves", Resource = Resources.high_elves },
                    new CardNameAndImage() { Name = "high elves", Resource = Resources.high_elves },
                    new CardNameAndImage() { Name = "legolas", Resource = Resources.legolas },
                    new CardNameAndImage() { Name = "legolas", Resource = Resources.legolas },
                    new CardNameAndImage() { Name = "merry pippin", Resource = Resources.merry_pippin },
                    new CardNameAndImage() { Name = "merry pippin", Resource = Resources.merry_pippin },
                    new CardNameAndImage() { Name = "one ring", Resource = Resources.one_ring },
                    new CardNameAndImage() { Name = "one ring", Resource = Resources.one_ring },
                    new CardNameAndImage() { Name = "ranger", Resource = Resources.ranger },
                    new CardNameAndImage() { Name = "ranger", Resource = Resources.ranger },
                    new CardNameAndImage() { Name = "samwise", Resource = Resources.samwise },
                    new CardNameAndImage() { Name = "samwise", Resource = Resources.samwise },
                    new CardNameAndImage() { Name = "smaug", Resource = Resources.smaug },
                    new CardNameAndImage() { Name = "smaug", Resource = Resources.smaug },
                    new CardNameAndImage() { Name = "tauriel", Resource = Resources.tauriel },
                    new CardNameAndImage() { Name = "tauriel", Resource = Resources.tauriel },
                    new CardNameAndImage() { Name = "thranduil", Resource = Resources.thranduil },
                    new CardNameAndImage() { Name = "thranduil", Resource = Resources.thranduil },
                    new CardNameAndImage() { Name = "treebeard", Resource = Resources.treebeard },
                    new CardNameAndImage() { Name = "treebeard", Resource = Resources.treebeard },
                    new CardNameAndImage() { Name = "troll", Resource = Resources.troll },
                    new CardNameAndImage() { Name = "troll", Resource = Resources.troll },
                    new CardNameAndImage() { Name = "witch king", Resource = Resources.witch_king },
                    new CardNameAndImage() { Name = "witch king", Resource = Resources.witch_king }
                }
            },
            { 2, new List<CardNameAndImage>()
                {
                    new CardNameAndImage() { Name = "BB8", Resource = Resources.BB8 },
                    new CardNameAndImage() { Name = "BB8", Resource = Resources.BB8 },
                    new CardNameAndImage() { Name = "chewbacca", Resource = Resources.chewbacca },
                    new CardNameAndImage() { Name = "chewbacca", Resource = Resources.chewbacca },
                    new CardNameAndImage() { Name = "Darth_Vader", Resource = Resources.Darth_Vader },
                    new CardNameAndImage() { Name = "Darth_Vader", Resource = Resources.Darth_Vader },
                    new CardNameAndImage() { Name = "Finn", Resource = Resources.Finn },
                    new CardNameAndImage() { Name = "Finn", Resource = Resources.Finn },
                    new CardNameAndImage() { Name = "jar_jar_binks", Resource = Resources.jar_jar_binks },
                    new CardNameAndImage() { Name = "jar_jar_binks", Resource = Resources.jar_jar_binks },
                    new CardNameAndImage() { Name = "kylo_ren", Resource = Resources.kylo_ren },
                    new CardNameAndImage() { Name = "kylo_ren", Resource = Resources.kylo_ren },
                    new CardNameAndImage() { Name = "Luke_Skywalker", Resource = Resources.Luke_Skywalker },
                    new CardNameAndImage() { Name = "Luke_Skywalker", Resource = Resources.Luke_Skywalker },
                    new CardNameAndImage() { Name = "mandalorian", Resource = Resources.mandalorian },
                    new CardNameAndImage() { Name = "mandalorian", Resource = Resources.mandalorian },
                    new CardNameAndImage() { Name = "maz_kanata", Resource = Resources.maz_kanata },
                    new CardNameAndImage() { Name = "maz_kanata", Resource = Resources.maz_kanata },
                    new CardNameAndImage() { Name = "obi_wan_kenobi", Resource = Resources.obi_wan_kenobi },
                    new CardNameAndImage() { Name = "obi_wan_kenobi", Resource = Resources.obi_wan_kenobi },
                    new CardNameAndImage() { Name = "poe", Resource = Resources.Poe },
                    new CardNameAndImage() { Name = "poe", Resource = Resources.Poe },
                    new CardNameAndImage() { Name = "r2d2", Resource = Resources.r2d2 },
                    new CardNameAndImage() { Name = "r2d2", Resource = Resources.r2d2 },
                    new CardNameAndImage() { Name = "sebulba", Resource = Resources.sebulba },
                    new CardNameAndImage() { Name = "sebulba", Resource = Resources.sebulba },
                    new CardNameAndImage() { Name = "Snoke", Resource = Resources.Snoke },
                    new CardNameAndImage() { Name = "Snoke", Resource = Resources.Snoke },
                    new CardNameAndImage() { Name = "x_wing", Resource = Resources.x_wing },
                    new CardNameAndImage() { Name = "x_wing", Resource = Resources.x_wing },
                    new CardNameAndImage() { Name = "yoda", Resource = Resources.yoda },
                    new CardNameAndImage() { Name = "yoda", Resource = Resources.yoda },


                }
            },

        };
        public Dictionary<int, Bitmap> CardFrontSide { get; private set; } = new Dictionary<int, Bitmap>()
        {
            { 0,  Resources.frontside },
            { 1,  Resources.lotr },
            { 2,  Resources.star_wars_logo },
        };

        public Dictionary<int, Bitmap> BackgroundTheme { get; private set; } = new Dictionary<int, Bitmap>()
        {
            { 0,  Resources.frontside },
            { 1,  Resources.lotr },
            { 2,  Resources.star_wars_logo },
        };
        #endregion

        public bool GameIsFrozen { get; private set; }
        public bool HasUnfinishedGame { get; private set; }
        public bool IsPlayerOnesTurn { get; private set; } = true;

        public List<Card> Deck { get; private set; }
        public List<Card> SelectedCards { get; private set; } //Holds 2 cards that currently are selected
        public List<KeyValuePair<int, string>> Theme { get; private set; } = new List<KeyValuePair<int, string>>();
        public List<GameOptions> GameOptions { get; private set; } = new List<GameOptions>() {
            new GameOptions { Name = "Classic 4x4", Rows = 4, Columns = 4 },
            new GameOptions { Name = "Easy 2x2", Rows = 2, Columns = 2 },
            new GameOptions { Name = "Medium 4x5", Rows = 4, Columns = 5 },
            new GameOptions { Name = "Hard 5x6", Rows = 5, Columns = 6 },
        };

        public Player[] Players { get; private set; } = new Player[2];

        public HighScore HighScores { get; private set; }
        public MainWindow Form1 { get; private set; }

        public int SelectedTheme { get; set; }
        public int Rows { get; set; } = 4;
        public int Collumns { get; set; } = 4;


        public Memory(MainWindow form1)
        {
            this.Form1 = form1;
            this.HighScores = new HighScore();
            this.Theme.Add(new KeyValuePair<int, string>(0, "Animals"));
            this.Theme.Add(new KeyValuePair<int, string>(1, "Lord Of The Rings"));
            this.Theme.Add(new KeyValuePair<int, string>(2, "Starwars"));

            //Check if there is a savefile that isn't empty
            Files.Create(this.SaveGamePath);
            if (Files.GetFileContent(this.SaveGamePath).Length > 0)
                this.HasUnfinishedGame = true;
        }

        /// <summary>
        /// Launches the memory game!
        /// </summary>
        public void StartGame()
        {
            this.PopulateDeck();
            this.ShuffleDeck();
            Sound.StartBackgroundMusic(this.SelectedTheme);
        }

        /// <summary>
        /// Ends the current memory game and redirects to highscores page.
        /// Calls the HighScores.AddToHighScores method to add the previously played game to the highscores
        /// </summary>
        public void EndGame()
        {
            string Results = $"{this.Players[0].Name} : {this.Players[0].ScoreBoard.Score} \n{this.Players[1].Name} : {this.Players[1].ScoreBoard.Score}";
            string title = "Scores";
            MessageBox.Show(Results, title);
            this.AddPlayersToHighScore();
            this.Form1.Dispatcher.Invoke(() =>
            {
                this.Form1.CleanupAfterGame();
            });
        }
        /// <summary>
        /// Resets the memory game
        /// </summary>
        public void ResetGame()
        {
            //If the game is already frozen prevent resetting.
            if (this.GameIsFrozen)
            {
                return;
            }
            this.GameIsFrozen = true;
            this.Form1.Dispatcher.Invoke(() =>
            {
                this.Form1.ClearPanels();
                this.PopulateDeck();
                foreach (Player player in this.Players)
                {
                    player.ScoreBoard.ResetScore();
                }
                this.Form1.Dispatcher.Invoke(() =>
                {
                    this.Form1.UpdateScoreBoardAndCurrentPlayer(this.Players[0], this.Players[1]);
                });
                this.ShuffleDeck();
                this.Form1.GeneratePlayingField();
            });
            this.GameIsFrozen = false;
        }

        /// <summary>
        /// Creates two instances of the Player class and gives them the user chosen names.
        /// </summary>
        /// <param name="playerOne"></param>
        /// <param name="playerTwo"></param>
        public void AddPlayers(string playerOne, string playerTwo)
        {
            this.Players[0] = new Player(playerOne);
            this.Players[1] = new Player(playerTwo);
        }

        /// <summary>
        /// Return the total amount of cards in the currently selected theme
        /// </summary>
        /// <returns>Integer total amount of cards</returns>
        public int TotalCardsInCurrentTheme()
        {
            return this.ThemeImages[this.SelectedTheme].Count / 2;
        }


        /// <summary>
        /// Adds both players to the highscores. 
        /// Afterwards clean up the array as preparation for the next game.
        /// </summary>
        private void AddPlayersToHighScore()
        {
            foreach (Player player in this.Players)
            {
                this.HighScores.AddToHighScores(player);
            }
            Array.Clear(this.Players, 0, this.Players.Length);
        }

        /// <summary>
        /// Freezes the game, converts current gamestate to JSON and stores it into the savefile.
        /// </summary>
        public void PauseGame()
        {
            this.GameIsFrozen = true;
            string json = this.ConvertGameStateToJson();
            this.SaveCurrentGameStateToSaveFile(json);
        }

        /// <summary>
        /// Write JSON to memory game savefile.
        /// </summary>
        /// <param name="json"></param>
        private void SaveCurrentGameStateToSaveFile(string json)
        {
            Files.Create(this.SaveGamePath);
            Files.WriteToFile(this.SaveGamePath, json);
        }

        /// <summary>
        /// Convert the current state of the game to JSON. 
        /// </summary>
        /// <returns></returns>
        private string ConvertGameStateToJson()
        {
            //dynamic gameState = new System.Dynamic.ExpandoObject();
            List<CardPictureBoxJson> jsonConvertableDeck = new List<CardPictureBoxJson>();

            foreach (Card card in this.Deck)
            {
                jsonConvertableDeck.Add(new CardPictureBoxJson(card.Name,
                                                               card.PairName,
                                                               card.IsSolved,
                                                               card.HasBeenVisible,
                                                               this.SelectedCards.Any(c => c.Name == card.Name)));
            }

            GameState gameState = new GameState(this.IsPlayerOnesTurn,
                                                this.SelectedTheme,
                                                jsonConvertableDeck,
                                                this.Rows,
                                                this.Collumns,
                                                this.Players);
            return JsonConvert.SerializeObject(gameState, Formatting.Indented);
        }

        /// <summary>
        /// Loads the content from the save file and applies it to the current game.
        /// </summary>
        private void ApplyStoredGameStateToCurrentGame()
        {
            string json = Files.GetFileContent(this.SaveGamePath);
            //Deserialize the json
            GameState gameState = JsonConvert.DeserializeObject<GameState>(json);
            //For the next part we need to cast/convert all properties that are stored in our dyanmic list to the appropriate type
            this.IsPlayerOnesTurn = gameState.IsPlayerOnesTurn;
            this.SelectedTheme = gameState.SelectedTheme;
            this.Rows = gameState.Rows;
            this.Collumns = gameState.Collumns;
            this.Players = gameState.Players;

            //It is important that we style our deck after we loaded in all the settings, otherwise we get a default 4*4 playing field
            //even though the settings may state otherwise
            List<CardPictureBoxJson> deck = new List<CardPictureBoxJson>();
            deck = gameState.Deck;

            foreach (CardPictureBoxJson jsonCard in deck)
            {
                CardNameAndImage pairNameAndImage = this.ThemeImages[this.SelectedTheme].Find(item => item.Name == jsonCard.PairName);
                Card card = new Card(pairNameAndImage.Name, pairNameAndImage.Resource)
                {
                    Name = jsonCard.Name,
                    IsSolved = jsonCard.IsSolved,
                    HasBeenVisible = jsonCard.HasBeenVisible
                };
                //Check if the card is currently selected, if so add it to the selectedCards list.
                if (jsonCard.IsSelected)
                {
                    this.SelectedCards.Add(card);
                }
                this.Deck.Add(card);
            }
        }

        /// <summary>
        /// Resumes paused game. If it needs to resume game from savefile load file contents back into the memory class
        /// and rebuild the deck.
        /// </summary>
        /// <param name="loadFromSaveFile"></param>
        public void ResumeGame(bool loadFromSaveFile = false)
        {
            if (loadFromSaveFile)
            {
                this.CreateEmptyDeckAndSelectedCardsList();
                this.ApplyStoredGameStateToCurrentGame();
                this.Form1.Dispatcher.Invoke(() =>
                {
                    this.Form1.UpdateScoreBoardAndCurrentPlayer(this.Players[0], this.Players[1]);
                    this.Form1.GeneratePlayingField();
                });
            }
            //Clear the current save file from data to prevent abuse
            this.SaveCurrentGameStateToSaveFile("");
            this.HasUnfinishedGame = false;
            //Pass back control to the player
            this.GameIsFrozen = false;
        }

        private void CreateEmptyDeckAndSelectedCardsList()
        {
            this.Deck = new List<Card>();
            this.SelectedCards = new List<Card>();
        }

        /// <summary>
        /// Generates the amount of cards needed based on this.Colums and this.Rows.
        /// Cards get assigned images based on the currently selected theme.
        /// Also handles randomizing the deck each time a game is played.
        /// </summary>
        private void PopulateDeck()
        {
            this.CreateEmptyDeckAndSelectedCardsList();
            for (int i = 0; i < (this.Rows * this.Collumns); i++)
            {
                Card card = new Card(this.ThemeImages[this.SelectedTheme][i].Name, this.ThemeImages[this.SelectedTheme][i].Resource)
                {
                    Name = $"b{i}", //Could make a property that holds an int but we need to cast it to a string later on anyway
                };
                this.Deck.Add(card);
            }
        }

        /// <summary>
        /// Shuffles the card in the deck to create a 'random' playing field
        /// </summary>
        private void ShuffleDeck()
        {
            this.Deck.Shuffle();
        }

        private void SetMatchingPairToSolved()
        {
            foreach (Card card in this.SelectedCards)
            {
                //When we have a matching pair mark them as solved to take them out of the game
                card.IsSolved = true;
            }
        }

        /// <summary>
        /// Increased the score of the current player
        /// </summary>
        private void AwardPlayerWithScore()
        {
            //Increment the score based on which player was playing
            if (this.IsPlayerOnesTurn)
                this.Players[0].ScoreBoard.IncreaseScore();
            else
                this.Players[1].ScoreBoard.IncreaseScore();
        }

        private void CheckAndHandlePlayerPunish()
        {
            //If there was no match and the card has previously been turned we want to punish the player
            //Check if any of the Cards in the this.SelectedCards list have the boolean HasBeenVisible flipped
            if (this.SelectedCards.FindIndex(c => c.HasBeenVisible == true) >= 0)
            {
                if (this.IsPlayerOnesTurn)
                    this.Players[0].ScoreBoard.DecreaseScore();
                else
                    this.Players[1].ScoreBoard.DecreaseScore();
            }
        }

        /// <summary>
        /// Hides both card images after a player failed to make a match
        /// </summary>
        private void HideFailedMatchCardImage()
        {
            foreach (Card card in this.SelectedCards)
            {
                //Reset the cards to show no image and set the property to ensure that we know the card has been flipped before
                card.Dispatcher.Invoke(() =>
                {
                    var imageSource = Memory.BitmapToImageSource(this.CardFrontSide[this.SelectedTheme]);
                    var image = new System.Windows.Controls.Image()
                    {
                        Source = imageSource,
                        Stretch = Stretch.Fill
                    };
                    card.Content = image;
                });
                card.HasBeenVisible = true;
            }
        }

        /// <summary>
        /// Checks if the game is finished or that the user can continue playing
        /// </summary>
        private void CheckEndGameConditions()
        {
            //Check if all cards are solved
            if (this.Deck.FindAll(c => c.IsSolved == false).Count == 0)
            {
                Sound.StopBackGroundMusic();
                Sound.PlayEffect(Resources.trumpets);
                this.EndGame();
            }
        }

        /// <summary>
        /// This method check if the selected card in SelectedCards list are a match.
        /// If it is a match flip a boolean in the PictureBox so that we know this card has previously been solved.
        /// Also calls the ScoreBoard.Add method to increment the score of the player that is currently playing.
        /// We keep track of the current player with the this.IsPlayerOnesTurn bool. After a player turned two cards
        /// this boolean is flipped.
        /// Lastly we clear the SelectedCards list so that we can keep on playing.
        /// </summary>
        public void CheckIfMatch()
        {
            if (this.SelectedCards[0].PairName == this.SelectedCards[1].PairName)
            {
                Sound.PlayEffect(Resources.correct);
                this.SetMatchingPairToSolved();
                this.AwardPlayerWithScore();
            }
            else
            {
                Sound.PlayEffect(Resources.incorrect);
                this.CheckAndHandlePlayerPunish();
                this.HideFailedMatchCardImage();
            }

            this.Form1.Dispatcher.Invoke(() =>
            {
                this.Form1.UpdateScoreBoardAndCurrentPlayer(this.Players[0], this.Players[1]);
            });

            this.IsPlayerOnesTurn = !this.IsPlayerOnesTurn;
            this.SelectedCards.Clear();
            this.GameIsFrozen = false;
            this.CheckEndGameConditions();
        }

        /// <summary>
        /// Handles whatever happends when clicking on one of the PictureBoxes(cards) on the playing field.
        /// First we have to run a couple checks to determine if we can proceed;
        /// - We need to check if the game is frozen, this happends after clicking on a card. We need to freeze the game
        ///   because if we do not do so the player has no time to see the second card he tried to match with the first.
        ///   So when we do not freeze the game the player has a (200)ms window to click other cards and mess up what is happenin
        /// - Check if the currently clicked card is already in the this.SelectedCards list, we do not want a user to be able
        ///   to gain points for matching the card with the card he just clicked.
        /// - If a card is solved we do not want to be able to gain points for that either.
        ///
        /// When we have two cards in the this.SelectedCards list we want to proceed and check if they match.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CardClicked(object sender, EventArgs e)
        {
            Card button = (Card)sender;
            if (button == null)
            {
                return;
            }
            Card selectedCard = this.Deck[Convert.ToInt32(button.Name.Substring(1))];
            //First check all the conditions on which we want to exit early
            if (this.GameIsFrozen || this.SelectedCards.Contains(selectedCard) || selectedCard.IsSolved)
            {
                return;
            }
            else
            {
                //Show the image that we stored in CardImage
                BitmapImage imageSource = Memory.BitmapToImageSource(selectedCard.CardImage);
                System.Windows.Controls.Image image = new System.Windows.Controls.Image()
                {
                    Source = imageSource,
                    Stretch = Stretch.Fill
                };
                button.Content = image;
                this.SelectedCards.Add(button);
            }

            //Only when 2 cards have been selected we want to freeze the game and check if they match
            if (this.SelectedCards.Count == 2)
            {
                this.GameIsFrozen = true;
                //Delay checking if they match with (200)ms. This is so that the user has a few ms to see what image they flipped
                //when they do not match. Without this its pretty much instant and the user wont be able to see what card they matched
                Task.Delay(200).ContinueWith(x =>
                {
                    this.CheckIfMatch();
                });
            }
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
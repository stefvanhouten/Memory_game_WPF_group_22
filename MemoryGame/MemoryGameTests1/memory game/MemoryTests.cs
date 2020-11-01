using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MemoryGame.Tests
{
    [TestClass()]
    public class MemoryTests
    {
        private const string PlayerOne = "p1";
        private const string PlayerTwo = "p2";
        public static MainWindow Form1 = new MainWindow();
        public static Memory MemoryGame = new Memory(Form1);

        [TestMethod()]
        public void ValidateDefault4x4Grid()
        {
            MemoryGame.SelectedTheme = 1;
            MemoryGame.AddPlayers(PlayerOne, PlayerTwo);
            MemoryGame.StartGame();
            Sound.StopBackGroundMusic();
            Assert.AreEqual(MemoryGame.Rows
                            * MemoryGame.Collumns, MemoryGame.Deck.Count);
            MemoryGame.EndGame();
        }

        [TestMethod()]
        public void Validate2x2Grid()
        {
            MemoryGame.Rows = MemoryGame.Collumns = 2;
            MemoryGame.AddPlayers(PlayerOne, PlayerTwo);
            MemoryGame.StartGame();
            Sound.StopBackGroundMusic();

            Assert.AreEqual(MemoryGame.Rows
                            * MemoryGame.Collumns, MemoryGame.Deck.Count);
            MemoryGame.EndGame();

        }

        [TestMethod()]
        public void Validate4x5Grid()
        {
            MemoryGame.Rows = 4;
            MemoryGame.Collumns = 5;
            MemoryGame.AddPlayers(PlayerOne, PlayerTwo);
            MemoryGame.StartGame();
            Sound.StopBackGroundMusic();

            Assert.AreEqual(MemoryGame.Rows
                            * MemoryGame.Collumns, MemoryGame.Deck.Count);
            MemoryGame.EndGame();
        }

        [TestMethod()]
        public void DeckIsRandom()
        {
            List<Card> deck1, deck2;

            MemoryGame.AddPlayers(PlayerOne, PlayerTwo);
            MemoryGame.StartGame();
            deck1 = MemoryGame.Deck;
            Sound.StopBackGroundMusic();
            MemoryGame.EndGame();

            MemoryGame.AddPlayers(PlayerOne, PlayerTwo);
            MemoryGame.StartGame();
            deck2 = MemoryGame.Deck;
            MemoryGame.EndGame();
            Sound.StopBackGroundMusic();

            Assert.AreNotEqual<List<Card>>(deck1, deck2);
        }

        [TestMethod()]
        public void BothPlayersAdded()
        {
            MemoryGame.AddPlayers(PlayerOne, PlayerTwo);

            Player playerOne = MemoryGame.Players[0];
            Player playerTwo = MemoryGame.Players[1];

            Assert.IsTrue(string.Equals(playerOne.Name,
                                        PlayerOne) && string.Equals(playerTwo.Name, PlayerTwo));
        }
    }
}
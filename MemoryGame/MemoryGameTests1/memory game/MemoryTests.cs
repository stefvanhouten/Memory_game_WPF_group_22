using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MemoryGame.Tests
{
    [TestClass()]
    public class MemoryTests
    {
        private const string PlayerOne = "p1";
        private const string PlayerTwo = "p2";

        [TestMethod()]
        public void ValidateDefault4x4Grid()
        {
            MainWindow form1 = new MainWindow();
            Memory memory = new Memory(form1);
            memory.SelectedTheme = 1;
            memory.StartGame();
            Sound.StopBackGroundMusic();

            Assert.AreEqual(memory.Rows
                            * memory.Collumns, memory.Deck.Count);
        }

        [TestMethod()]
        public void Validate2x2Grid()
        {
            MainWindow form1 = new MainWindow();
            Memory memory = new Memory(form1);
            memory.Rows = memory.Collumns = 2;
            memory.StartGame();
            Sound.StopBackGroundMusic();

            Assert.AreEqual(memory.Rows
                            * memory.Collumns, memory.Deck.Count);
        }

        [TestMethod()]
        public void Validate4x5Grid()
        {
            MainWindow form1 = new MainWindow();
            Memory memory = new Memory(form1);
            memory.Rows = 4;
            memory.Collumns = 5;
            memory.StartGame();
            Sound.StopBackGroundMusic();

            Assert.AreEqual(memory.Rows
                            * memory.Collumns, memory.Deck.Count);
        }

        [TestMethod()]
        public void DeckIsRandom()
        {
            MainWindow form1 = new MainWindow();
            Memory memory = new Memory(form1);
            List<Card> deck1, deck2;

            memory.AddPlayers(PlayerOne, PlayerTwo);
            memory.StartGame();
            deck1 = memory.Deck;
            Sound.StopBackGroundMusic();
            memory.EndGame();

            memory.AddPlayers(PlayerOne, PlayerTwo);
            memory.StartGame();
            deck2 = memory.Deck;
            memory.EndGame();
            Sound.StopBackGroundMusic();

            Assert.AreNotEqual<List<Card>>(deck1, deck2);
        }

        [TestMethod()]
        public void BothPlayersAdded()
        {
            MainWindow form1 = new MainWindow();
            Memory memory = new Memory(form1);
            memory.AddPlayers(PlayerOne, PlayerTwo);

            Player playerOne = memory.Players[0];
            Player playerTwo = memory.Players[1];

            Assert.IsTrue(string.Equals(playerOne.Name,
                                        PlayerOne) && string.Equals(playerTwo.Name, PlayerTwo));
        }
    }
}
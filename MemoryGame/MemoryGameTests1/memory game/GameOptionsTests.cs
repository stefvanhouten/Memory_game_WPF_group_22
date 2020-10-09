using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Tests
{
    [TestClass()]
    public class GameOptionsTests
    {
        [TestMethod()]
        public void CalculateCardsRequired()
        {
            GameOptions gameOptions = new GameOptions() { Columns = 4, Rows = 4, Name = "default 4x4" };
            Assert.AreEqual(gameOptions.Columns * gameOptions.Rows, gameOptions.CardsRequired);
        }
    }
}
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
    public class ScoreBoardTests
    {

        [TestMethod()]
        public void IncrementPlayerOneScore()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.IncreaseScore();
            ScoreBoard scoreBoardTwo = new ScoreBoard();

            Assert.IsTrue(int.Equals(scoreBoard.Score,
                                     scoreBoard.IncreaseScoreWith) && int.Equals(scoreBoardTwo.Score, 0));
        }


        [TestMethod()]
        public void ScoreboardIsZeroAtGameStart()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            Assert.AreEqual(scoreBoard.Score, 0);
        }

        [TestMethod()]
        public void ValidateScoreCantGoBelowZero()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.DecreaseScore();

            Assert.AreEqual(scoreBoard.Score, 0);
        }

    }
}
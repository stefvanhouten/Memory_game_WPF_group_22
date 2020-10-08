using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class GameState
    {
        public bool isPlayerOnesTurn { get; set; }
        public int SelectedTheme { get; set; }
        public List<CardPictureBoxJson> Deck { get; set; }
        public int Rows { get; set; }
        public int Collumns { get; set; }
        public Player[] Players { get; set; }
    }
}

using System.Collections.Generic;

namespace MemoryGame
{
    public class GameState
    {
        public bool IsPlayerOnesTurn { get; private set; }
        public int SelectedTheme { get; private set; }
        public List<CardPictureBoxJson> Deck { get; private set; }
        public int Rows { get; private set; }
        public int Collumns { get; private set; }
        public Player[] Players { get; private set; }
        public bool AudioEnabled { get; private set; }

        public GameState(bool isPlayerOnesTurn,
                         int selectedTheme,
                         List<CardPictureBoxJson> deck,
                         int rows,
                         int collumns,
                         Player[] players,
                         bool audioEnabled)
        {
            this.IsPlayerOnesTurn = isPlayerOnesTurn;
            this.SelectedTheme = selectedTheme;
            this.Deck = deck;
            this.Rows = rows;
            this.Collumns = collumns;
            this.Players = players;
            this.AudioEnabled = audioEnabled;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public GameState(bool isPlayerOnesTurn, int selectedTheme, List<CardPictureBoxJson> deck, int rows, int collumns, Player[] players)
        {
            this.IsPlayerOnesTurn = isPlayerOnesTurn;
            this.SelectedTheme = selectedTheme;
            this.Deck = deck;
            this.Rows = rows;
            this.Collumns = collumns;
            this.Players = players;
        }
    }
}

﻿namespace MemoryGame
{
    /// <summary>
    /// Base class for a player object in the memory game
    /// </summary>
    public class Player
    {
        public string Name { get; private set; }
        public ScoreBoard ScoreBoard { get; private set; }

        /// <summary>
        /// Create a new instance of a player object. 
        /// Also attaches the ScoreBoard class to the player.
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            this.Name = name;
            this.ScoreBoard = new ScoreBoard();
        }
    }
}

﻿namespace MemoryGame
{
    /// <summary>
    /// Template that the user can use to decide game size for their memory game
    /// </summary>
    public class GameOptions
    {
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int CardsRequired
        {
            get
            {
                return this.Rows * this.Columns;
            }
        }
        //Method used to display the game size in ComboBox
        public override string ToString()
        {
            return this.Name;
        }
    }
}


using System.Drawing;
using System.Windows.Controls;

namespace MemoryGame
{
    public class Card : Button
    {
        //public string Name { get; set; }
        public string PairName { get; set; }
        public bool IsSolved { get; set; } = false;
        public bool HasBeenVisible { get; set; } = false;
        public Bitmap CardImage { get; set; } //Custom bitmap to store the image in. PictureBox.Image cant be hidden without losing the image
        public Bitmap Image { get; set; }
    }

    class CardPictureBoxJson
    {
        public string Name { get; set; }
        public string PairName { get; set; }
        public bool IsSolved { get; set; } = false;
        public bool HasBeenVisible { get; set; } = false;
        public bool IsSelected { get; set; } = false;
    }
}

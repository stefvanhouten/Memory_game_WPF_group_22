
using System.Drawing;
using System.Windows.Controls;

namespace MemoryGame
{
    public class Card : Button
    {
        public string PairName { get; private set; }
        public bool IsSolved { get; set; }
        public bool HasBeenVisible { get; set; }
        public Bitmap CardImage { get; private set; } //Custom bitmap to store the image in. PictureBox.Image cant be hidden without losing the image

        public Card(string pairName, Bitmap cardImage)
        {
            this.PairName = pairName;
            this.CardImage = cardImage;
        }
    }
}

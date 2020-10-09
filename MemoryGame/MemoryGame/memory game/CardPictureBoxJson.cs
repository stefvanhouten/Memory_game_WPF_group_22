namespace MemoryGame
{
    public class CardPictureBoxJson
    {
        public string Name { get; private set; }
        public string PairName { get; private set; }
        public bool IsSolved { get; private set; }
        public bool HasBeenVisible { get; private set; }
        public bool IsSelected { get; private set; }

        public CardPictureBoxJson(string name, string pairName, bool isSolved, bool hasBeenVisible, bool isSelected)
        {
            this.Name = name;
            this.PairName = pairName;
            this.IsSolved = isSolved;
            this.HasBeenVisible = hasBeenVisible;
            this.IsSelected = isSelected;
        }
    }
}

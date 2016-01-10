namespace LostMonitors.Core
{
    public class Turn
    {
        public Turn(Card card)
        {
            Card = card;
        }

        public Card Card { get; private set; }
        public bool Discard { get; set; }
        public Destination? Draw { get; set; }
    }
}

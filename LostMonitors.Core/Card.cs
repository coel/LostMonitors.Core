namespace LostMonitors.Core
{
    public class Card
    {
        internal Card(Destination destination, CardValue value)
        {
            Destination = destination;
            Value = value;
        }

        public Destination Destination { get; private set; }
        public CardValue Value { get; private set; }
    }
}

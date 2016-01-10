namespace LostMonitors.Core.Engine
{
    public class GlobalBoardTurn
    {
        internal GlobalBoardTurn(Turn turn, Card draw)
        {
            PlayCard = turn.Card;
            PlayIsDiscard = turn.Discard;
            DrawCard = draw;
            DrawLocation = turn.Draw;
        }
        public Card PlayCard { get; private set; }
        public bool PlayIsDiscard { get; private set; }
        public Card DrawCard { get; private set; }
        public Destination? DrawLocation { get; private set; }
    }
}

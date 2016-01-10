using System.Collections.Generic;
using System.Linq;
using LostMonitors.Core.Engine;

namespace LostMonitors.Core
{
    public class BoardState
    {
        internal BoardState(Deck deck, Dictionary<Destination, Stack<Card>> discards, EnginePlayer you, EnginePlayer them)
        {
            CardsRemaining = deck.Count;

            Discards = new Dictionary<Destination, Card>(discards.Keys.Count);
            
            foreach (var destination in discards.Keys)
            {
                var cards = discards[destination];
                if (cards.Any())
                {
                    Discards.Add(destination, cards.Peek());
                }
            }

            YourExpeditions = you.GetExpeditions();
            TheirExpeditions = them.GetExpeditions();

            YourCards = you.Cards.ToList();
        }

        public int CardsRemaining { get; set; }
        public Dictionary<Destination, Card> Discards { get; set; }
        public Dictionary<Destination, List<Card>> YourExpeditions { get; set; }
        public Dictionary<Destination, List<Card>> TheirExpeditions { get; set; }
        public List<Card> YourCards { get; set; }
    }
}

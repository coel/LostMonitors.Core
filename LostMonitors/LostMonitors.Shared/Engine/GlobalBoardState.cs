using System.Collections.Generic;
using System.Linq;

namespace LostMonitors.Engine
{
    public class GlobalBoardState
    {
        internal GlobalBoardState(EnginePlayer player1, EnginePlayer player2, Deck deck, Dictionary<Destination, Stack<Card>> discards)
        {
            CardsRemaining = deck.Count;

            Discards = new Dictionary<Destination, Card>(discards.Keys.Count);
            foreach (var destination in discards.Keys)
            {
                Discards.Add(destination, discards[destination].Peek());
            }

            Player1Expeditions = player1.GetExpeditions();
            Player2Expeditions = player2.GetExpeditions();
            Player1Cards = player1.Cards.ToList(); // Maybe fishy way of creating a new list
            Player2Cards = player2.Cards.ToList(); // Maybe fishy way of creating a new list
        }

        public int CardsRemaining { get; private set; }
        public Dictionary<Destination, Card> Discards { get; private set; }
        public Dictionary<Destination, List<Card>> Player1Expeditions { get; private set; }
        public Dictionary<Destination, List<Card>> Player2Expeditions { get; private set; }
        public List<Card> Player1Cards { get; private set; }
        public List<Card> Player2Cards { get; private set; }
    }
}

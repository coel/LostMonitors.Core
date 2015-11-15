using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostMonitors.Engine;

namespace LostMonitors
{
    public class BoardState
    {
        internal BoardState(Deck deck, Dictionary<Destination, Stack<Card>> discards, EnginePlayer you, EnginePlayer them)
        {
            CardsRemaining = deck.Count;

            Discards = new Dictionary<Destination, Card>(discards.Keys.Count);
            
            foreach (var destination in discards.Keys)
            {
                Discards.Add(destination, discards[destination].Peek());
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

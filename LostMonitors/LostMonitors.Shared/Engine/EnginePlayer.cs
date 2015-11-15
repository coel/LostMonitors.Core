using System;
using System.Collections.Generic;
using System.Linq;

namespace LostMonitors.Engine
{
    internal class EnginePlayer
    {
        public IPlayer Player { get; private set;  }
        public List<Card> Cards { get; private set; }
        private readonly Dictionary<Destination, Stack<Card>> _expeditions = new Dictionary<Destination, Stack<Card>>();

        public EnginePlayer(IPlayer player)
        {
            Player = player;

            var destinations = Enum.GetValues(typeof(Destination)).Cast<Destination>().ToList();
            foreach (var destination in destinations)
            {
                _expeditions.Add(destination, new Stack<Card>());
            }

            Cards = new List<Card>(Engine.InitialCards);
        }

        public Dictionary<Destination, List<Card>> GetExpeditions()
        {
            var result = new Dictionary<Destination, List<Card>>(_expeditions.Keys.Count);
            foreach (var destination in _expeditions.Keys)
            {
                result.Add(destination, _expeditions[destination].ToList());
            }
            return result;
        }

        public bool AddExpedition(Card card)
        {
            var destination = _expeditions[card.Destination];
            if (destination.Any())
            {
                var top = destination.Peek().Value;

                if (top != CardValue.Investment && top < card.Value)
                {
                    // Sanity check, it shouldn't end up here
                    throw new Exception(string.Format("Some how got two of {0}, {1}", card.Destination, card.Value));
                }

                if (top > card.Value)
                {
                    return false;
                }
            }
            destination.Push(card);
            return true;
        }
    }
}

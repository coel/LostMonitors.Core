using System;
using System.Collections.Generic;
using System.Linq;

namespace LostMonitors.Engine
{
    internal class Deck
    {
        private readonly Stack<Card> _deck;
        private static readonly Random Rng = new Random();

        public Deck()
        {
            var values = Enum.GetValues(typeof(CardValue)).Cast<CardValue>().ToList();
            var destinations = Enum.GetValues(typeof(Destination)).Cast<Destination>().ToList();

            var cards = new List<Card>((values.Count + 2)*destinations.Count);

            foreach (var destination in destinations)
            {
                foreach (var value in values)
                {
                    cards.Add(new Card(destination, value));
                }

                // To make 3 investment cards altogether
                cards.Add(new Card(destination, CardValue.Investment));
                cards.Add(new Card(destination, CardValue.Investment));
            }

            Shuffle(cards);

            _deck = new Stack<Card>(cards);
        }

        private static void Shuffle(IList<Card> cards)
        {
            var n = cards.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }  
        }

        public Card Draw()
        {
            return _deck.Pop();
        }

        public int Count { get { return _deck.Count; } }
    }
}

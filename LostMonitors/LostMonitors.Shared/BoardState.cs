using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMonitors
{
    public class BoardState
    {
        public int CardsRemaining { get; set; }
        public Dictionary<Destination, Card> Discards { get; set; }
        public Dictionary<Destination, List<Card>> YourExpeditions { get; set; }
        public Dictionary<Destination, List<Card>> TheirExpeditions { get; set; }
        public List<Card> YourCards { get; set; }
    }
}

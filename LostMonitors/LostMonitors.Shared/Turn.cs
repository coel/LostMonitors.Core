using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMonitors
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

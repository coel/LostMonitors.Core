using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMonitors.Shared.Extensions
{
    public static class ListCardExtensions
    {
        public static int ExpeditionValue(this List<Card> cards)
        {
            var multiplier = cards.Count(x => x.Value == CardValue.Investment) + 1;
            var sum = cards.Sum(x => (int)x.Value);
            return sum * multiplier;
        }
    }
}

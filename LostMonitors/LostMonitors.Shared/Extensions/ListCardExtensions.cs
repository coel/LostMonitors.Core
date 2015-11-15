using System.Collections.Generic;
using System.Linq;

namespace LostMonitors.Extensions
{
    public static class ListCardExtensions
    {
        public static int ExpeditionValue(this IList<Card> cards)
        {
            var multiplier = cards.Count(x => x.Value == CardValue.Investment) + 1;
            var sum = cards.Sum(x => (int)x.Value);
            return sum * multiplier;
        }
    }
}
